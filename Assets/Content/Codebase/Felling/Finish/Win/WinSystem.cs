using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Common.Tweens;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Tree;
using Woodman.Locations.Trees;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Utils;

namespace Woodman.Felling.Finish.Win
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
        private EcsOneData<SecondChanceData> _secondChance;
        private PlayerCoinsRepository _coinsRepository;

        public void PostRun()
        {
            var q = _world.Select<WinEvent>();
            if (!q.Any())
                return;

            _treesRepository.SetFell(_treesRepository.CurrentTree.Id);
            _progressionService.RegisterCoreResult(true);
            _world.CreateEvent(new FellingFinishSignal
            {
                reason = FellingFinishReason.Win,
                progress = 1,
                secondChanceShowed = _secondChance.GetData().wasShowed
            });
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
                _windows.FellingWinWindow.SetCoinsCost(10);
                _windows.FellingWinWindow.SetCoins(_coinsRepository.GetPlayerRes());
                _windows.FellingWinWindow.ResetX2();
                _world.NewEntity().AddComponent(tweenData).AddComponent(new WinX2TweenTag());
            }

            _windows.FellingWinWindow.Show();
        }
    }
}