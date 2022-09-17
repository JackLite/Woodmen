using System.Threading.Tasks;
using Woodman.Common;

namespace Woodman.Cheats
{
    public class CheatsModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();
            AddDependency(mainViewProvider.DebugViewProvider);
            AddDependency(mainViewProvider.DebugViewProvider.DebugResourceViewProvider);
            mainViewProvider.DebugViewProvider.gameObject.SetActive(true);
            mainViewProvider.DebugViewProvider.DebugPanel.SetActive(false);
            mainViewProvider.DebugViewProvider.DebugResourceViewProvider.gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}