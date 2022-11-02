using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.MetaTrees;

namespace Woodman.Felling.Win
{
    [EcsSystem(typeof(FellingModule))]
    public class WinSystem : IPostRunSystem
    {
        private DataWorld _world;
        private MetaTreesRepository _treesRepository;
        private WindowsUiProvider _windows;
        public void PostRun()
        {
            var q = _world.Select<WinEvent>();
            if (!q.Any())
                return;
            
            _treesRepository.SetFell(_treesRepository.CurrentTree.Id);
            _world.DeactivateModule<FellingModule>();
            _windows.FellingWinWindow.Show();
        }
    }
}