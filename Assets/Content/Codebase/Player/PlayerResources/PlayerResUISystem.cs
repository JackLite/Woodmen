using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Meta;

namespace Woodman.Player.PlayerResources
{
    [EcsSystem(typeof(MetaModule))]
    public class PlayerResUISystem : IInitSystem, IDestroySystem
    {
        private MetaUiProvider _metaUiProvider;
        private PlayerLogsRepository _playerLogsRepository;
        private PlayerCoinsRepository _playerCoinsRepository;

        public void Init()
        {
            _playerLogsRepository.OnChange += LogsChanges;
            _playerCoinsRepository.OnChange += CoinsChanges;

            _metaUiProvider.LogsBarMetaUI.SetLogsCount(_playerLogsRepository.GetPlayerRes());
            _metaUiProvider.CoinsBarMetaUI.SetCoinsCount(_playerCoinsRepository.GetPlayerRes());
        }

        private void CoinsChanges(int arg1, int arg2)
        {
            //todo: устанавливать с анимацией
            _metaUiProvider.CoinsBarMetaUI.SetCoinsCount(arg2);
        }

        private void LogsChanges(int arg1, int arg2)
        {
            //todo: устанавливать с анимацией
            _metaUiProvider.LogsBarMetaUI.SetLogsCount(arg2);
        }

        public void Destroy()
        {
            _playerLogsRepository.OnChange -= LogsChanges;
            _playerCoinsRepository.OnChange -= CoinsChanges;
        }
    }
}