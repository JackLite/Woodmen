using System.Linq;
using Woodman.Utils;

namespace Woodman.Progress
{
    public class TreeProgressionService
    {
        private const string SaveKey = "core.location.tree";
        private int _saved;
        private readonly TreeProgressionSettings _settings;
        private int _currentLocation;
        private int _currentTreeIndex;

        public TreeProgressionService(TreeProgressionSettings settings)
        {
            _settings = settings;
            _saved = SaveUtility.LoadInt(SaveKey);
            _currentLocation = _saved / (10 * 6);
            _currentTreeIndex = _saved % (10 * 6);
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

        public void SetLocation(int locationIndex)
        {
            _currentLocation = locationIndex;
            Save();
        }

        public void SetFell()
        {
            _currentTreeIndex++;
            Save();
        }

        private void Save()
        {
            _saved = _currentLocation * 10 * 6 + _currentTreeIndex;
        }
    }
}