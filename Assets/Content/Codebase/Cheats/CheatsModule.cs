using System.Threading.Tasks;
using Woodman.Cheats.View;
using Woodman.Utils;

namespace Woodman.Cheats
{
    public class CheatsModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            var debugViewProvider = GetGlobalDependency<StartupModule, DebugViewProvider>();
            AddDependency(debugViewProvider);
            AddDependency(debugViewProvider.DebugResourceViewProvider);
            debugViewProvider.gameObject.SetActive(true);
            debugViewProvider.DebugPanel.SetActive(false);
            debugViewProvider.DebugResourceViewProvider.gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}