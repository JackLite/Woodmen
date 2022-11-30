using Newtonsoft.Json;
using Unity.Mathematics;
using UnityEngine.AddressableAssets;
using Woodman.Felling.Tree.Progression;
using Woodman.Locations;
using Woodman.Utils;

namespace Woodman.Progress
{
    public class ProgressionService
    {
        private const string LocationsSaveKey = "core.location.current";
        private const string BuildingSaveKey = "core.building.count";
        private const string TreeProgressionKey = "core.tree.treeProgressionSaveData";
        private readonly ProgressionSettings _settings;
        private readonly LocationsSettings _locationsSettings;
        private int _currentLocation;
        private int _buildingsCount;
        private int _totalBuildingsCount;
        private TreeProgressionSaveData _treeProgression = new();

        public ProgressionService(ProgressionSettings settings, LocationsSettings locationsSettings)
        {
            _settings = settings;
            _locationsSettings = locationsSettings;
            _currentLocation = SaveUtility.LoadInt(LocationsSaveKey);
            _buildingsCount = SaveUtility.LoadInt(BuildingSaveKey);
            if (SaveUtility.IsKeyExist(TreeProgressionKey))
            {
                var raw = SaveUtility.LoadString(TreeProgressionKey);
                _treeProgression = JsonConvert.DeserializeObject<TreeProgressionSaveData>(raw);
            }

            #if UNITY_EDITOR
            if (settings.debug)
            {
                _currentLocation = settings.currentLocationIndex;
            }
            #endif
        }

        public int GetSize()
        {
            var progressionInfo = _settings.treeProgressionInfo[_currentLocation];
            var size = TreeSizeCalculator.CalculateTreeSize(
                _treeProgression.lastResult,
                progressionInfo,
                _treeProgression.treesProgress);
            _treeProgression.lastDifficult = size.difficult;
            return size.size;
        }

        public void RegisterCoreResult(bool isWin)
        {
            _treeProgression.lastResult.isWin = isWin;
            var difficultChanged = _treeProgression.lastResult.difficult != _treeProgression.lastDifficult;
            if (isWin)
            {
                _treeProgression.treesProgress[_treeProgression.lastDifficult]++;
                if (difficultChanged)
                    _treeProgression.lastResult.count = 1;
                else
                    _treeProgression.lastResult.count++;
            }
            else
            {
                if (difficultChanged)
                    _treeProgression.lastResult.count = 2;
                else
                    _treeProgression.lastResult.count--;
            }
            _treeProgression.lastResult.difficult = _treeProgression.lastDifficult;
            _treeProgression.lastResult.count = math.clamp(_treeProgression.lastResult.count, 1, 2);

            SaveUtility.SaveString(TreeProgressionKey, JsonConvert.SerializeObject(_treeProgression), true);
        }

        public int GetLocationIndex()
        {
            return _currentLocation;
        }

        public AssetReference GetLocation()
        {
            return _locationsSettings.locations[_currentLocation];
        }

        public void RegisterFinishBuilding()
        {
            _buildingsCount++;
            SaveUtility.SaveInt(BuildingSaveKey, _buildingsCount, true);
        }

        public void ChangeLocation(int locationIndex)
        {
            _currentLocation = locationIndex;
            _treeProgression = new TreeProgressionSaveData();
            SaveUtility.SaveInt(LocationsSaveKey, _currentLocation);
            SaveUtility.SaveString(TreeProgressionKey, JsonConvert.SerializeObject(_treeProgression), true);
        }

        public void SetBuildingsCount(int count)
        {
            _totalBuildingsCount = count;
        }

        public bool IsBuildingsFinished()
        {
            return _buildingsCount >= _totalBuildingsCount;
        }
    }
}