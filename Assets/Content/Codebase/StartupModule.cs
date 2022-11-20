using System.Threading.Tasks;
using ModulesFramework.Attributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Woodman.Buildings;
using Woodman.Cheats;
using Woodman.Cheats.View;
using Woodman.Common;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Settings;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Win;
using Woodman.Loading;
using Woodman.Locations;
using Woodman.Locations.Boat;
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Player;
using Woodman.Player.PlayerResources;
using Woodman.Progress;
using Woodman.Settings;
using Woodman.Utils;

namespace Woodman
{
    [GlobalModule]
    public class StartupModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            if (SceneManager.GetActiveScene().name != "MainScene")
            {
                await Addressables.LoadSceneAsync("MainScene").Task;
            }
            Application.targetFrameRate = 300;
            var uiProvider = Object.FindObjectOfType<UiProvider>(true);
            AddDependency(uiProvider);
            BindView(uiProvider);
            
            uiProvider.LoadScreen.SetProgress(.1f);
            CreateOneData();

            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);

            var progressionSettings =
                await Addressables.LoadAssetAsync<ProgressionSettings>("ProgressionSettings").Task;
            AddDependency(progressionSettings);
            var locations = await Addressables.LoadAssetAsync<LocationsSettings>("LocationsContainer").Task;
            uiProvider.LoadScreen.SetProgress(.3f);

            AddDependency(locations);
            AddDependency(new PlayerCoinsRepository());
            AddDependency(new MetaTreesRepository());
            var logsHeapRepository = new LogsHeapRepository();
            AddDependency(logsHeapRepository);
            AddDependency(new LogsHeapService(logsHeapRepository));
            AddDependency(new ProgressionService(progressionSettings, locations));
            var buildingsRepository = new BuildingsRepository();
            AddDependency(buildingsRepository);
            var boatSaveService = new BoatSaveService();
            AddDependency(boatSaveService);
            var playerLogsRepository = new PlayerLogsRepository();
            AddDependency(playerLogsRepository);
            AddDependency(new BuildingService(buildingsRepository, playerLogsRepository, boatSaveService));

            var locationsChooseView = Object.FindObjectOfType<LocationsView>(true);
            AddDependency(locationsChooseView);
            var debugViewProvider = Object.FindObjectOfType<DebugViewProvider>(true);
            AddDependency(debugViewProvider);
            
            uiProvider.LoadScreen.SetProgress(.6f);

            var visualSettings = await Addressables.LoadAssetAsync<VisualSettings>("VisualSettings").Task;
            AddDependency(visualSettings);
            uiProvider.LoadScreen.SetProgress(.9f);
        }

        private void CreateOneData()
        {
            CreateOneData(new PlayerData { maxWoodCount = 50 });
            CreateOneData<TreeModel>();
            CreateOneData<TimerData>();
            CreateOneData<LocationData>();
            CreateOneData<DebugStateData>();
            CreateOneData(new SecondChanceData { remainTime = 5, totalTime = 5 });
        }
    }
}