using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using EcsCore;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Locations;
using Woodman.Logs;
using Woodman.MetaTrees;

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
                var location = await Addressables.InstantiateAsync("VikingsLocation").Task;
                var locationView = location.GetComponent<LocationView>();
                locationView.SetBuildingsStates(_buildingsRepository);
                locationView.SetTreesStates(_treesRepository);
                
                await LoadLogs();

                _mainViewProvider.WoodmanContainer.transform.position = locationView.GetPlayerSpawnPos();
                await Task.Delay(TimeSpan.FromSeconds(1f));
                _world.InitModule<MainModule>();
                _world.InitModule<FellingModule, MainModule>();
                _world.ActivateModule<MainModule>();
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
                var warmCount = (int)(count * 1.2f);
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