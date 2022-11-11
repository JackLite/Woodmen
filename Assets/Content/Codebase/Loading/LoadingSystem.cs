using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Locations;
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Progress;
using Object = UnityEngine.Object;

namespace Woodman.Loading
{
    [EcsSystem(typeof(StartupModule))]
    public class LoadingSystem : IInitSystem
    {
        private BuildingsRepository _buildingsRepository;
        private LogsHeapRepository _logsHeapRepository;
        private DataWorld _world;
        private LocationsView _locationsView;
        private EcsOneData<LocationsData> _locationsData;

        public void Init()
        {
            Load();
        }

        private async void Load()
        {
            try
            {
                AssetReference chosenLocation = null;
                var locations = await Addressables.LoadAssetAsync<Locations>("LocationsContainer").Task;
                _locationsView.Init(locations.locations, locations.names);
                _locationsView.gameObject.SetActive(true);
                _locationsView.OnOnLocationChosen += r => chosenLocation = r;
                while (chosenLocation == null)
                    await Task.Delay(200);

                var ld = _locationsData.GetData();
                ld.currentLocation = chosenLocation;
                _locationsData.SetData(ld);

                _locationsView.gameObject.SetActive(false);

                _world.InitModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}