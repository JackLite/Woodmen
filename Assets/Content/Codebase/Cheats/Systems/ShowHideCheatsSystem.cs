using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Cheats.View;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class ShowHideCheatsSystem : IInitSystem, IDestroySystem
    {
        private DebugViewProvider _debugViewProvider;
        private EcsOneData<DebugStateData> _state;

        public void Init()
        {
            _debugViewProvider.ShowDebugPanelBtn.onClick.AddListener(ShowHidePanel);
        }

        public void Destroy()
        {
            _debugViewProvider.ShowDebugPanelBtn.onClick.RemoveListener(ShowHidePanel);
        }

        private void ShowHidePanel()
        {
            ref var state = ref _state.GetData();
            state.isShowed = !state.isShowed;
            _debugViewProvider.DebugPanel.SetActive(state.isShowed);
        }
    }
}