using System;
using System.Threading.Tasks;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Locations;
using Woodman.Logs;
using Woodman.Progress;

namespace Woodman.Loading
{
    [EcsSystem(typeof(StartupModule))]
    public class LoadingSystem : IInitSystem
    {
        private BuildingsRepository _buildingsRepository;
        private LogsHeapRepository _logsHeapRepository;
        private DataWorld _world;
        private LocationsView _locationsView;
        private LocationsSettings _locations;
        private EcsOneData<LocationData> _locationsData;
        private ProgressionService _progressionService;
        private LoadingView _loadingView;

        public void Init()
        {
            Load();
        }

        private async void Load()
        {
            try
            {
                if (Debug.isDebugBuild && _locations.choseLocation)
                {
                    await ChooseLocation();
                }
                else
                {
                    LoadLastLocation();
                }

                _world.InitModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void LoadLastLocation()
        {
            var location = _progressionService.GetLocation();
            ref var ld = ref _locationsData.GetData();
            ld.currentLocation = location;
        }

        private async Task ChooseLocation()
        {
            AssetReference chosenLocation = null;
            _locationsView.Init(_locations.locations, _locations.names);
            _locationsView.gameObject.SetActive(true);
            _locationsView.OnOnLocationChosen += r => chosenLocation = r;
            while (chosenLocation == null)
                await Task.Delay(200);

            var ld = _locationsData.GetData();
            ld.currentLocation = chosenLocation;
            _locationsData.SetData(ld);

            _locationsView.gameObject.SetActive(false);
        }
    }
}