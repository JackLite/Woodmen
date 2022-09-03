using System;
using System.Threading.Tasks;
using Core;
using EcsCore;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Common;
using Woodman.EcsCodebase.Felling;
using Woodman.EcsCodebase.Locations;

namespace Woodman.EcsCodebase.Loading
{
    [EcsSystem(typeof(StartupModule))]
    public class LoadingSystem : IInitSystem
    {
        private MainViewProvider _mainViewProvider;
        private DataWorld _world;
        
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