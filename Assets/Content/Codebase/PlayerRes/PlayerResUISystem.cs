using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;


namespace Woodman.PlayerRes
{
    [EcsSystem(typeof(MainModule))]
    public class PlayerResUISystem : IInitSystem, IDestroySystem
    {
        private ResourceBarMetaUI _resourceBar;
        private PlayerResRepository _playerResRepository;
        public void Init()
        {
            _playerResRepository.OnChange += OnResChanges;
            _resourceBar.SetCount(_playerResRepository.GetPlayerRes());
        }

        public void Destroy()
        {
            _playerResRepository.OnChange -= OnResChanges;
        }
        
        private void OnResChanges(int arg1, int arg2)
        {
            //todo: устанавливать с анимацией
            _resourceBar.SetCount(arg2);
        }
    }
}