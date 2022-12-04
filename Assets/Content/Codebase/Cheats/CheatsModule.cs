using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Woodman.Cheats.View;
using Woodman.Utils;

namespace Woodman.Cheats
{
    public class CheatsModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            var debugCanvas = await Addressables.InstantiateAsync("DebugCanvas").Task;
            var debugViewProvider = debugCanvas.GetComponent<DebugViewProvider>();
            AddDependency(debugViewProvider);
            AddDependency(debugViewProvider.DebugResourceViewProvider);
            debugViewProvider.gameObject.SetActive(true);
            debugViewProvider.DebugPanel.SetActive(false);
            debugViewProvider.DebugResourceViewProvider.gameObject.SetActive(false);
            
        }
    }
}