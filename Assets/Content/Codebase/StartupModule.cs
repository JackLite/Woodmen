using System.Threading.Tasks;
using ModulesFramework.Attributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Cheats;
using Woodman.Cheats.View;
using Woodman.Common;
using Woodman.Felling.Settings;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Loading;
using Woodman.Locations;
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Player;
using Woodman.Utils;

namespace Woodman
{
    [GlobalModule]
    public class StartupModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            Application.targetFrameRate = 60;
            CreateOneData(new PlayerOneData {maxWoodCount = 50});
            CreateOneData<TreeModel>();
            CreateOneData<TimerData>();
            CreateOneData<LocationsData>();
            CreateOneData<DebugStateData>();

            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);
            
            AddDependency(new BuildingsRepository());
            AddDependency(new MetaTreesRepository());
            AddDependency(new LogsHeapRepository());
            
            var locationsChooseView = Object.FindObjectOfType<LocationsView>(true);
            AddDependency(locationsChooseView);
            var debugViewProvider = Object.FindObjectOfType<DebugViewProvider>(true);
            AddDependency(debugViewProvider);
            var uiProvider = Object.FindObjectOfType<UiProvider>(true);
            AddDependency(uiProvider);
            BindView(uiProvider);
            
            var visualSettings = await Addressables.LoadAssetAsync<VisualSettings>("VisualSettings").Task;
            AddDependency(visualSettings);
        }
    }
}