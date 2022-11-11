using System.Threading.Tasks;
using ModulesFramework.Attributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Cheats;
using Woodman.Cheats.View;
using Woodman.Common;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Settings;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Loading;
using Woodman.Locations;
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Player;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Utils;

namespace Woodman
{
    [GlobalModule]
    public class StartupModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            Application.targetFrameRate = 60;
            CreateOneData(new PlayerData {maxWoodCount = 50});
            CreateOneData<TreeModel>();
            CreateOneData<TimerData>();
            CreateOneData<LocationsData>();
            CreateOneData<DebugStateData>();
            CreateOneData(new SecondChanceData { remainTime = 5, totalTime = 5});

            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);

            var treeProgressionSettings =
                await Addressables.LoadAssetAsync<TreeProgressionSettings>("TreeProgressionSettings").Task;
            AddDependency(new BuildingsRepository());
            AddDependency(new TreeProgressionService(treeProgressionSettings));
            AddDependency(new LogsHeapRepository());
            AddDependency(new MetaTreesRepository());
            AddDependency(new PlayerLogsRepository());
            AddDependency(new PlayerCoinsRepository());

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