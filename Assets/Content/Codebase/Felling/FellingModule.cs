using System.Threading.Tasks;
using Woodman.Common;
using Woodman.Felling.Settings;

namespace Woodman.Felling
{
    public class FellingModule : EcsModuleWithDependencies
    {
        private FellingSettings _fellingSettings;

        protected override Task Setup()
        {
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();
            mainViewProvider.WindowsUiProvider.MetaUi.gameObject.SetActive(true);
            mainViewProvider.WindowsUiProvider.LoadScreen.SetActive(false);
            return Task.CompletedTask;
        }
    }
}