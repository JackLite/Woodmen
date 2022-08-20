using System;
using Woodman.Felling.Timer;

namespace Woodman.Felling.Tree
{
    /// <summary>
    ///     Инкапсулирует работу с данными в коре
    /// </summary>
    public class TreeProgressService
    {
        private readonly FellingTimer _fellingTimer;
        private readonly FellingSettingsContainer _settingsContainer;
        private int _remainSize;
        private TreeModel _treeModel;

        public TreeProgressService(FellingTimer fellingTimer, FellingSettingsContainer settingsContainer)
        {
            _fellingTimer = fellingTimer;
            _settingsContainer = settingsContainer;
        }

        public event Action OnProgressChange;

        public void SetModel(TreeModel treeModel)
        {
            _treeModel = treeModel;
            _remainSize = treeModel.size;
        }

        public int GetRemain()
        {
            return _remainSize;
        }

        public int GetTotalSize()
        {
            return _treeModel.size;
        }

        public void UpdateAfterCut()
        {
            _remainSize--;
            _fellingTimer.AddTime(_settingsContainer.GetSettings().timeForCut);
            OnProgressChange?.Invoke();
        }
    }
}