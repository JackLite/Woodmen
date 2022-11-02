using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Cheats.View;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class ShowHideResSystem : IInitSystem, IDestroySystem
    {
        private DebugViewProvider _viewProvider;
        public void Init()
        {
            _viewProvider.SetResourcesBtn.onClick.AddListener(ShowResourcesPanel);
            _viewProvider.DebugResourceViewProvider.CloseBtn.onClick.AddListener(HideResourcesPanel);
        }

        private void ShowResourcesPanel()
        {
            _viewProvider.DebugResourceViewProvider.gameObject.SetActive(true);
        }

        private void HideResourcesPanel()
        {
            _viewProvider.DebugResourceViewProvider.gameObject.SetActive(false);
        }

        public void Destroy()
        {
            _viewProvider.SetResourcesBtn.onClick.RemoveListener(ShowResourcesPanel);
            _viewProvider.DebugResourceViewProvider.CloseBtn.onClick.RemoveListener(HideResourcesPanel);
        }
    }
}