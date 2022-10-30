using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Common;
using Woodman.Felling.Settings;
using Woodman.Felling.Timer;

namespace Woodman.Felling
{
    public class FellingModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            var fellingSettings = GetOneData<FellingSettings, MainModule>().GetData();
            CreateOneData(new TimerData
            {
                remain = fellingSettings.time,
                totalTime = fellingSettings.time
            });
            
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();
            mainViewProvider.WindowsUiProvider.MetaUi.gameObject.SetActive(true);
            mainViewProvider.WindowsUiProvider.LoadScreen.SetActive(false);
            return Task.CompletedTask;
        }
    }
}