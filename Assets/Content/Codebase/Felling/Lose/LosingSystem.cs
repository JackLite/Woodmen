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

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(CoreModule))]
    public class LosingSystem : IPostRunSystem
    {
        private DataWorld _world;
        private EcsOneData<DebugStateData> _debugData;
        private EcsOneData<SecondChanceData> _secondChanceData;
        private EcsOneData<TreeModel> _currentTree;
        private FellingCharacterController _character;
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
                if (scd.wasShowed)
                {
                    _windows.FellingLoseWindow.Show();
                }
                else
                {
                    scd.wasShowed = true;
                    scd.isActive = true;
                    scd.loseReason = reason;
                    _windows.SecondChanceView.SetCost(5);
                    var progress = 1 - (float)_piecesRepository.GetRemain() / _currentTree.GetData().size;
                    _windows.SecondChanceView.SetProgress(progress);
                    _windows.SecondChanceView.SetLoseReason(reason);
                    _windows.SecondChanceView.Show();
                }

                _world.DeactivateModule<FellingModule>();
            });
        }
    }
}