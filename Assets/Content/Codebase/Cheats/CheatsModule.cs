using System.Collections.Generic;
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

            world.NewEntity().AddComponent(new DebugTestComponent
            {
                testStr = "test",
                listOfList = new List<List<int>>
                {
                    new List<int> {1, 2, 3},
                    new List<int>{4, 5, 6}
                },
                d = 5.65,
                f = 3.04f,
                integer = -150,
                names = new List<string> {"Alice", "Bob", "Carl"},
                someDict = new Dictionary<string, List<int>>
                {
                    {"first key", new List<int>{10, 11, 12}},
                    {"second", new List<int>{13, 14, 15}},
                }
            })
            .AddComponent(new DebugStateData());
        }
    }
}