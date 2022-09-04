using Core;
using EcsCore;
using Woodman.Common;
using Woodman.Felling.Taps;
using Woodman.Felling.Timer;

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(MainModule))]
    public class LosingSystem : IPostRunSystem
    {
        private DataWorld _world;
        private WindowsUiProvider _windows;
        public void PostRun()
        {
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
        }

        private void GameOver()
        {
            _windows.FellingLoseWindow.Show();
            _world.DeactivateModule<FellingModule>();
        }
    }
}