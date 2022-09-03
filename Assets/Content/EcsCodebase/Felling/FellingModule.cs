using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Common;
using Woodman.EcsCodebase.Felling.Timer;

namespace Woodman.EcsCodebase.Felling
{
    public class FellingModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);
            
            CreateOneData(new TimerData
            {
                remain = fellingSettings.time,
                totalTime = fellingSettings.time
            });
            
            
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();
            mainViewProvider.WindowsUiProvider.MetaUi.gameObject.SetActive(true);
            mainViewProvider.WindowsUiProvider.LoadScreen.SetActive(false);
        }
    }
}