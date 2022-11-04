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
using Woodman.Logs;
using Woodman.MetaTrees;
using Object = UnityEngine.Object;

namespace Woodman.Loading
{
    [EcsSystem(typeof(StartupModule))]
    public class LoadingSystem : IInitSystem
    {
        private BuildingsRepository _buildingsRepository;
        private LogsHeapRepository _logsHeapRepository;
        private MetaTreesRepository _treesRepository;
        private DataWorld _world;
        private MainViewProvider _mainViewProvider;

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
                _mainViewProvider.LocationsView.Init(locations.locations, locations.names);
                _mainViewProvider.LocationsView.gameObject.SetActive(true);
                _mainViewProvider.LocationsView.OnOnLocationChosen += r => chosenLocation = r;
                while (chosenLocation == null)
                    await Task.Delay(200);
                _mainViewProvider.LocationsView.gameObject.SetActive(false);
                var t = await Addressables.LoadSceneAsync(chosenLocation, LoadSceneMode.Additive).Task;
                t.ActivateAsync();
                LightProbes.TetrahedralizeAsync();
                var locationView = Object.FindObjectOfType<LocationView>();
                locationView.SetBuildingsStates(_buildingsRepository);
                locationView.SetTreesStates(_treesRepository);
                
                await LoadLogs();

                await _mainViewProvider.PoolsProvider.BuildingFxPool.WarmUp(3);
                _mainViewProvider.WoodmanContainer.transform.position = locationView.GetPlayerSpawnPos();
                _world.InitModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async Task LoadLogs()
        {
            var counter = new Dictionary<LogsHeapType, int>();
            foreach (var heapData in _logsHeapRepository.GetData())
            {
                if (!counter.ContainsKey(heapData.type))
                    counter[heapData.type] = 0;
                counter[heapData.type]++;
            }

            foreach (var (type, count) in counter)
            {
                var warmCount = math.min(1, (int)(count * 1.2f));
                await _mainViewProvider.LogsPool.WarmUp(type, math.max(3, warmCount));
            }

            foreach (var heapData in _logsHeapRepository.GetData())
            {
                var view = _mainViewProvider.LogsPool.GetLogView(heapData.type);
                view.Id = heapData.id;
                view.SetCount(heapData.count);
                view.transform.position = heapData.position;
                view.Show();
            }
        }
    }
}