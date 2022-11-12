using System.Linq;
using UnityEngine.AddressableAssets;
using Woodman.Locations;
using Woodman.Utils;

namespace Woodman.Progress
{
    public class ProgressionService
    {
        private const string LocationsSaveKey = "core.location.current";
        private const string BuildingSaveKey = "core.building.count";
        private const string TreeSaveKey = "core.tree.current";
        private readonly ProgressionSettings _settings;
        private readonly LocationsSettings _locationsSettings;
        private int _currentLocation;
        private int _buildingsCount;
        private int _currentTreeIndex;
        private int _totalBuildingsCount;

        public ProgressionService(ProgressionSettings settings, LocationsSettings locationsSettings)
        {
            _settings = settings;
            _locationsSettings = locationsSettings;
            _currentLocation = SaveUtility.LoadInt(LocationsSaveKey);
            _buildingsCount = SaveUtility.LoadInt(BuildingSaveKey);
            _currentTreeIndex = SaveUtility.LoadInt(TreeSaveKey);
            #if UNITY_EDITOR
            if (settings.debug)
            {
                _currentLocation = settings.currentLocationIndex;
                _currentTreeIndex = settings.currentTreeIndex;
            }
            #endif
        }

        public int GetSize()
        {
            var info = _settings.treeProgressionInfo[_currentLocation];
            if (_currentTreeIndex < info.easyTrees.Length)
                return info.easyTrees[_currentTreeIndex];

            var newIndex = _currentTreeIndex - info.easyTrees.Length;
            if (newIndex < info.middleTrees.Length)
                return info.middleTrees[newIndex];

            newIndex = _currentTreeIndex - info.middleTrees.Length;
            if (newIndex < info.hardTrees.Length)
                return info.hardTrees[newIndex];

            return info.hardTrees.Last();
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

        public void SetLocation(int locationIndex)
        {
            _currentLocation = locationIndex;
            SaveUtility.SaveInt(LocationsSaveKey, _currentLocation, true);
        }

        public void SetFell()
        {
            _currentTreeIndex++;
            SaveUtility.SaveInt(TreeSaveKey, _currentTreeIndex, true);
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