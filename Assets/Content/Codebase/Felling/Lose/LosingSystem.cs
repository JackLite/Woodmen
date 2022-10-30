using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Felling.Taps;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree.Branches;

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(MainModule))]
    public class LosingSystem : IPostRunSystem
    {
        private DataWorld _world;
        private EcsOneData<DebugStateData> _debugData;
        private WindowsUiProvider _windows;
        public void PostRun()
        {
            if (_debugData.GetData().isGodModeTurnOn)
                return;
            var endTimer = _world.Select<TimerEndEvent>();
            if (endTimer.Any())
            {
                GameOver();
                endTimer.DestroyAll();
            }
            
            var branchCollideQ = _world.Select<BranchCollide>();
            if (branchCollideQ.Any())
            {
                GameOver();
                branchCollideQ.DestroyAll();
            }
            
            var hiveCollideQ = _world.Select<HiveCollideEvent>();
            if (hiveCollideQ.Any())
            {
                GameOver();
                hiveCollideQ.DestroyAll();
            }
        }

        private void GameOver()
        {
            _windows.FellingLoseWindow.Show();
            _world.DeactivateModule<FellingModule>();
        }
    }
}