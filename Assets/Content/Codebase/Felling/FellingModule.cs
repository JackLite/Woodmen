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
        private FellingSettings _fellingSettings;

        protected override Task Setup()
        {
            _fellingSettings = GetOneData<FellingSettings, MainModule>().GetData();
            CreateOneData(new TimerData
            {
                remain = _fellingSettings.time,
                totalTime = _fellingSettings.time
            });
            
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();
            mainViewProvider.WindowsUiProvider.MetaUi.gameObject.SetActive(true);
            mainViewProvider.WindowsUiProvider.LoadScreen.SetActive(false);
            return Task.CompletedTask;
        }

        public override void OnActivate()
        {
            var td = GetOneData<TimerData, FellingModule>();
            td.SetData(new TimerData
            {
                remain = _fellingSettings.time,
                totalTime = _fellingSettings.time
            });
        }
    }
}