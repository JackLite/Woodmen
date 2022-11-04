using System.Threading.Tasks;
using ModulesFramework.Attributes;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Felling.Settings;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Logs;
using Woodman.Meta;
using Woodman.MetaTrees;
using Woodman.Player;

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
            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);
            AddDependency(new BuildingsRepository());
            AddDependency(new MetaTreesRepository());
            AddDependency(new LogsHeapRepository());
            var viewProvider = Object.FindObjectOfType<MainViewProvider>(true);
            AddDependency(viewProvider);
            BindView(viewProvider);
            var uiProvider = Object.FindObjectOfType<UiProvider>(true);
            AddDependency(uiProvider);
            BindView(uiProvider);
            CreateOneData<DebugStateData>();
            var transparency = await Addressables.LoadAssetAsync<VisualSettings>("VisualSettings").Task;
            AddDependency(transparency);
        }
    }
}