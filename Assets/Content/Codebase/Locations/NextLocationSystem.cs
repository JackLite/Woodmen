using System;
using System.Threading.Tasks;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Woodman.Buildings;
using Woodman.Common.UI;
using Woodman.Locations.Trees;
using Woodman.Meta;
using Woodman.Progress;

namespace Woodman.Locations
{
    [EcsSystem(typeof(MetaModule))]
    public class NextLocationSystem : IRunSystem
    {
        private DataWorld _world;
        private InnerLoadingScreen _innerLoadingScreen;
        private ProgressionService _progressionService;
        private EcsOneData<LocationData> _locationData;
        private LocationsSettings _locations;
        private MetaViewProvider _metaView;
        private BuildingsRepository _buildingsRepository;
        private MetaTreesRepository _treesRepository;

        public void Run()
        {
            var q = _world.Select<NextLocationEvent>();
            if (!q.Any())
                return;

            q.DestroyAll();
            NextLocation();
        }

        private async void NextLocation()
        {
            try
            {
                // show load screen
                _innerLoadingScreen.Show();
                await Task.Delay(TimeSpan.FromSeconds(_innerLoadingScreen.animDuration));

                // destroy location
                var ld = _locationData.GetData();
                ld.locationView = null;
                await Addressables.UnloadSceneAsync(ld.currentLocationScene).Task;

                // load new location
                _progressionService.SetLocation(_progressionService.GetLocationIndex() + 1);
                var newLocation = _locations.locations[_progressionService.GetLocationIndex()];
                var scene = await Addressables.LoadSceneAsync(newLocation, LoadSceneMode.Additive).Task;
                scene.ActivateAsync();
                LightProbes.TetrahedralizeAsync();

                ld.currentLocation = newLocation;
                ld.currentLocationScene = scene;
                ld.locationView = UnityEngine.Object.FindObjectOfType<LocationView>();
                _locationData.SetData(ld);
                ld.locationView.SetBuildingsStates(_buildingsRepository);
                ld.locationView.SetTreesStates(_treesRepository);
                _progressionService.SetBuildingsCount(ld.locationView.GetBuildingsCount());

                // place character
                _metaView.WoodmanContainer.transform.position = ld.locationView.GetPlayerSpawnPos();

                // hide load screen
                _innerLoadingScreen.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}