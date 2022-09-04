using Core;
using EcsCore;
using Woodman.Common;

namespace Woodman.Felling.Win
{
    [EcsSystem(typeof(FellingModule))]
    public class WinSystem : IPostRunSystem
    {
        private DataWorld _world;
        private WindowsUiProvider _windows;
        public void PostRun()
        {
            var q = _world.Select<WinEvent>();
            if (!q.Any())
                return;
            
            _world.DeactivateModule<FellingModule>();
            _windows.FellingWinWindow.Show();
        }
    }
}