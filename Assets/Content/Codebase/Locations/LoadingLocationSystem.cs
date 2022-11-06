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
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Meta;

namespace Woodman.Locations
{
    [EcsSystem(typeof(MetaModule))]
    public class LoadingLocationSystem : IInitSystem
    {
        private EcsOneData<LocationsData> _locationsData;
        private BuildingsRepository _buildingsRepository;
        private LogsHeapRepository _logsHeapRepository;
        private MetaTreesRepository _treesRepository;
        private MetaViewProvider _metaViewProvider;
        
        private DataWorld _world;
        
        public void Init()
        {
            Load();
        }

        private async void Load()
        {
            try
            {
                var ld = _locationsData.GetData();
                var t = await Addressables.LoadSceneAsync(ld.currentLocation, LoadSceneMode.Additive).Task;
                t.ActivateAsync();
                LightProbes.TetrahedralizeAsync();
                var locationView = UnityEngine.Object.FindObjectOfType<LocationView>();
                locationView.SetBuildingsStates(_buildingsRepository);
                locationView.SetTreesStates(_treesRepository);

                await LoadLogs();

                await _metaViewProvider.PoolsProvider.BuildingFxPool.WarmUp(3);
                _metaViewProvider.WoodmanContainer.transform.position = locationView.GetPlayerSpawnPos();
                _world.ActivateModule<MetaModule>();
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
                await _metaViewProvider.LogsPool.WarmUp(type, math.max(3, warmCount));
            }

            foreach (var heapData in _logsHeapRepository.GetData())
            {
                var view = _metaViewProvider.LogsPool.GetLogView(heapData.type);
                view.Id = heapData.id;
                view.SetCount(heapData.count);
                view.transform.position = heapData.position;
                view.Show();
            }
        }
    }
}