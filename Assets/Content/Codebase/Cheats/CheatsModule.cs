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
            CreateOneData<DebugStateData>();
            mainViewProvider.DebugViewProvider.gameObject.SetActive(true);
            return Task.CompletedTask;
        }
    }
}