using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Common.Tweens;
using Woodman.Felling.Tree;
using Woodman.Locations.Trees;
using Woodman.Player.PlayerResources;
using Woodman.Progress;

namespace Woodman.Felling.Win
{
    [EcsSystem(typeof(FellingModule))]
    public class WinSystem : IPostRunSystem
    {
        private DataWorld _world;
        private ProgressionService _progressionService;
        private LogsHeapService _logsHeapService;
        private MetaTreesRepository _treesRepository;
        private UiProvider _windows;
        private EcsOneData<TreeModel> _treeModel;
        private PlayerCoinsRepository _coinsRepository;
        
        
        public void PostRun()
        {
            var q = _world.Select<WinEvent>();
            if (!q.Any())
                return;
            
            _treesRepository.SetFell(_treesRepository.CurrentTree.Id);
            _progressionService.SetFell();
            _logsHeapService.SaveLogs(ref _treeModel.GetData());
            _world.DeactivateModule<FellingModule>();
            _windows.FellingUi.Hide();
            ShowWinWindow();
        }

        private void ShowWinWindow()
        {
            if (_coinsRepository.GetPlayerRes() < 10)
            {
                _windows.FellingWinWindow.HideX2();
            }
            else
            {
                var tweenData = new TweenData
                {
                    remain = 5,
                    update = r => { _windows.FellingWinWindow.SetSliderProgress(r / 5f); },
                    onEnd = _windows.FellingWinWindow.HideX2,
                    validate = () => _windows.FellingWinWindow != null
                };
                var treeModel = _treeModel.GetData();
                _windows.FellingWinWindow.SetLogsCount(treeModel.size);
                _world.NewEntity().AddComponent(tweenData).AddComponent(new WinX2TweenTag());
            }

            _windows.FellingWinWindow.Show();
        }
    }
}