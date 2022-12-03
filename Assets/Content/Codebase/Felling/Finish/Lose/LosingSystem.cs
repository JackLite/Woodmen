using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Common.Delay;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Taps;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Branches;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Utils;

namespace Woodman.Felling.Finish.Lose
{
    [EcsSystem(typeof(CoreModule))]
    public class LosingSystem : IPostRunSystem
    {
        private DataWorld _world;
        private EcsOneData<DebugStateData> _debugData;
        private EcsOneData<SecondChanceData> _secondChanceData;
        private EcsOneData<TreeModel> _currentTree;
        private FellingCharacterController _character;
        private PlayerCoinsRepository _coinsRepository;
        private ProgressionService _progressionService;
        private TreePiecesRepository _piecesRepository;
        private UiProvider _windows;

        public void PostRun()
        {
            if (_debugData.GetData().isGodModeTurnOn)
                return;
            var endTimer = _world.Select<TimerEndEvent>();
            if (endTimer.Any())
            {
                GameOver(LoseReason.TimeOut);
                endTimer.DestroyAll();
            }

            var branchCollideQ = _world.Select<BranchCollide>();
            if (branchCollideQ.Any())
            {
                GameOver(LoseReason.BranchCollide);
                branchCollideQ.DestroyAll();
            }

            var hiveCollideQ = _world.Select<HiveCollideEvent>();
            if (hiveCollideQ.Any())
            {
                GameOver(LoseReason.HiveCollide);
                hiveCollideQ.DestroyAll();
            }
        }

        private void GameOver(LoseReason reason)
        {
            _windows.FellingUi.Hide();
            _character.Dead();
            DelayedFactory.Create(_world, 1f, () =>
            {
                ref var scd = ref _secondChanceData.GetData();
                if (scd.wasShowed || _coinsRepository.GetPlayerRes() < 5)
                {
                    _windows.FellingLoseWindow.Show();
                    _world.CreateEvent(new FellingFinishSignal
                    {
                        reason = FellingFinishReason.Lose,
                        progress = _currentTree.GetData().progress,
                        secondChanceShowed = scd.wasShowed
                    });
                    _progressionService.RegisterCoreResult(false);
                }
                else
                {
                    scd.wasShowed = true;
                    scd.isActive = true;
                    scd.loseReason = reason;
                    _windows.SecondChanceView.SetCost(5);
                    _windows.SecondChanceView.SetCoins(_coinsRepository.GetPlayerRes());
                    var progress = 1 - (float)_piecesRepository.GetRemain() / _currentTree.GetData().size;
                    _windows.SecondChanceView.SetProgress(progress);
                    _windows.SecondChanceView.SetLoseReason(reason);
                    _windows.SecondChanceView.Show();
                    DelayedFactory.Create(_world, .5f, () =>
                    {
                        _windows.SecondChanceView.ActivateSkip();
                    });
                }

                _world.DeactivateModule<FellingModule>();
            });
        }
    }
}