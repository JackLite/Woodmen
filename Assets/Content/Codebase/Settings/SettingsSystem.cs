using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Meta;

namespace Woodman.Settings
{
    [EcsSystem(typeof(MetaModule))]
    public class SettingsSystem : IInitSystem, IDestroySystem
    {
        private SettingsWindow _settingsWindow;
        private MetaUiProvider _metaUiProvider;
        public void Init()
        {
            _metaUiProvider.SettingsButton.onClick.AddListener(_settingsWindow.Show);
        }

        public void Destroy()
        {
            _metaUiProvider.SettingsButton.onClick.RemoveListener(_settingsWindow.Show);
        }
    }
}