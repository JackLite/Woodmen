using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Buildings;
using Woodman.Cheats.View;
using Woodman.Locations;
using Woodman.Locations.Trees;
using Woodman.Progress;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class ResetCurrentLocationSystem : IInitSystem, IDestroySystem
    {
        private BuildingsRepository _buildingsRepository;
        private DebugViewProvider _provider;
        private EcsOneData<LocationData> _location;
        private MetaTreesRepository _treesRepository;
        private ProgressionService _progressionService;

        public void Init()
        {
            _provider.ResetCurrentLocationBtn.onClick.AddListener(ResetLocation);
        }

        public void Destroy()
        {
            _provider.ResetCurrentLocationBtn.onClick.RemoveListener(ResetLocation);
        }

        private void ResetLocation()
        {
            ref var location = ref _location.GetData();
            var buildings = location.locationView.GetBuildings();
            foreach (var building in buildings)
            {
                building.SetState(0);
                building.SetLogs(0, building.GetResForState(1));
                _buildingsRepository.SetBuildingLogsCount(0, building.Id);
                _buildingsRepository.SetBuildingStateIndex(0, building.Id);
            }

            var trees = location.locationView.GetTrees();
            foreach (var tree in trees)
            {
                tree.ShowTree();
                _treesRepository.Reset(tree.Id);
            }
        }
    }
}