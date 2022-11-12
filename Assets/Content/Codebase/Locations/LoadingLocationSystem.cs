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
using Woodman.Locations.Trees;
using Woodman.Logs;
using Woodman.Meta;
using Woodman.Player;
using Woodman.Progress;

namespace Woodman.Locations
{
    [EcsSystem(typeof(MetaModule))]
    public class LoadingLocationSystem : IInitSystem
    {
        private EcsOneData<LocationData> _locationsData;
        private EcsOneData<PlayerData> _playerData;
        private BuildingsRepository _buildingsRepository;
        private LogsHeapRepository _logsHeapRepository;
        private MetaTreesRepository _treesRepository;
        private MetaViewProvider _metaViewProvider;
        private ProgressionService _progressionService;

        private DataWorld _world;

        public void Init()
        {
            Load();
        }

        private async void Load()
        {
            try
            {
                await LoadLocation();
                await LoadLogs();
                await _metaViewProvider.PoolsProvider.BuildingFxPool.WarmUp(3);

                _world.ActivateModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async Task LoadLocation()
        {
            var ld = _locationsData.GetData();
            var sceneInstance = await Addressables.LoadSceneAsync(ld.currentLocation, LoadSceneMode.Additive).Task;
            sceneInstance.ActivateAsync();
            LightProbes.TetrahedralizeAsync();

            ld.currentLocationScene = sceneInstance;
            ld.locationView = UnityEngine.Object.FindObjectOfType<LocationView>();
            _locationsData.SetData(ld);
            ld.locationView.SetBuildingsStates(_buildingsRepository);
            ld.locationView.SetTreesStates(_treesRepository);
            _progressionService.SetBuildingsCount(ld.locationView.GetBuildingsCount());
            if (_progressionService.IsBuildingsFinished())
                ld.locationView.ShowBoat();
            UpdatePlayerPos(ld.locationView);
        }

        private void UpdatePlayerPos(LocationView locationView)
        {
            Vector3 pos;
            if (_playerData.GetData().metaPos == Vector3.zero)
                pos = locationView.GetPlayerSpawnPos();
            else
                pos = _playerData.GetData().metaPos;
            _metaViewProvider.WoodmanContainer.transform.position = pos;
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