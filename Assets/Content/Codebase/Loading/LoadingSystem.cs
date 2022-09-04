using System;
using System.Threading.Tasks;
using Core;
using EcsCore;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Locations;

namespace Woodman.Loading
{
    [EcsSystem(typeof(StartupModule))]
    public class LoadingSystem : IInitSystem
    {
        private BuildingsRepository _buildingsRepository;
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
    }
}