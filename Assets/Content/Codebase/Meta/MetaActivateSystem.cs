using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Common;

namespace Woodman.Meta
{
    [EcsSystem(typeof(MetaModule))]
    public class MetaActivateSystem : IActivateSystem
    {
        private UiProvider _uiProvider;
        public void Activate()
        {
            _uiProvider.LoadScreen.SetActive(false);
            _uiProvider.MetaUi.Show();
            _uiProvider.InnerLoadingScreen.Hide();
        }
    }
}