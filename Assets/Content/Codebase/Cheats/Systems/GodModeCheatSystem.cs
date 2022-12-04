using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Cheats.View;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class GodModeCheatSystem : IInitSystem, IDestroySystem
    {
        private DebugViewProvider _viewProvider;
        private EcsOneData<DebugStateData> _godModeState;
        public void Init()
        {
            _viewProvider.GodModeBtn.onClick += SwitchGodMode;
        }

        private void SwitchGodMode()
        {
            ref var state = ref _godModeState.GetData();
            state.isGodModeTurnOn = !state.isGodModeTurnOn;
            if (state.isGodModeTurnOn)
            {
                _viewProvider.DebugMessageView.SetMsg("God mode turn on!");
                _viewProvider.GodModeBtn.SetText("Turn off God Mode");
            }
            else
            {
                _viewProvider.DebugMessageView.SetMsg("God mode turn off!");
                _viewProvider.GodModeBtn.SetText("Turn on God Mode");
            }
        }

        public void Destroy()
        {
            _viewProvider.GodModeBtn.onClick -= SwitchGodMode;
        }
    }
}