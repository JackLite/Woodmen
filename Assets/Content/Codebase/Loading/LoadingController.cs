using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Common;
using Woodman.Locations;
using Zenject;

namespace Woodman.Loading
{
    public class LoadingController : IInitializable
    {
        private readonly WindowsUiProvider _windowsUiProvider;
        private readonly GameObject _woodman;

        public LoadingController(WindowsUiProvider windowsUiProvider, MainViewProvider mainViewProvider)
        {
            _windowsUiProvider = windowsUiProvider;
            _woodman = mainViewProvider.WoodmanContainer;
        }

        public void Initialize()
        {
            Load();
        }

        private async void Load()
        {
            try
            {
                var location = await Addressables.InstantiateAsync("VikingsLocation").Task;
                var locationView = location.GetComponent<LocationView>();
                _woodman.transform.position = locationView.GetPlayerSpawnPos();
                await Task.Delay(TimeSpan.FromSeconds(1f));
                _windowsUiProvider.LoadScreen.SetActive(false);
                _windowsUiProvider.MetaUi.gameObject.SetActive(true);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}