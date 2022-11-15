using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using Woodman.Common;
using Woodman.Locations;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Logs.LogsUsing;
using Woodman.Player.PlayerResources;
using Woodman.Progress;

namespace Woodman.Buildings
{
    [EcsSystem(typeof(MetaModule))]
    public class BuildingInteractSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerLogsRepository _resRepository;
        private BuildingsRepository _buildingsRepository;
        private PoolsProvider _poolsProvider;
        private ProgressionService _progressionService;
        private BuildingService _buildingService;
        private EcsOneData<LocationData> _locationData;
        private VisualSettings _visualSettings;

        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(c => c.interactType == InteractTypeEnum.Building);
            if (!q.TrySelectFirst(out Interact interact))
                return;

            OnInteract(interact.target);
        }

        private void OnInteract(InteractTarget target)
        {
            var interact = target.GetComponent<BuildingInteract>();
            if (interact == null)
                return;

            if (_buildingsRepository.IsLastState(interact.BuildingView))
                return;

            Process(interact);
        }

        private void Process(BuildingInteract interact)
        {
            var delta = _buildingService.CalcHouseNextState(interact.BuildingView);
            var oldState = _buildingsRepository.GetBuildingStateIndex(interact.BuildingView.Id);
            _resRepository.SubtractRes(delta.totalResources);
            _buildingsRepository.SetBuildingStateIndex(delta.resultState, interact.BuildingView.Id);
            _buildingsRepository.SetBuildingLogsCount(delta.resultLogsCount, interact.BuildingView.Id);

            if (delta.totalResources > 0)
            {
                _world.NewEntity().AddComponent(new UsingLogsCreateEvent
                {
                    count = math.min(delta.totalResources, _visualSettings.usingLogsCount),
                    to = interact.BuildingView.transform.position,
                    delayBetween = _visualSettings.usingLogsDelayBetween
                });
            }

            if (oldState != delta.resultState)
            {
                interact.BuildingView.AnimateTo(delta.resultState, _poolsProvider.BuildingFxPool);
                if (delta.resultState == interact.BuildingView.StatesCount - 1)
                {
                    FinishBuilding();
                }
            }
        }

        private void FinishBuilding()
        {
            _progressionService.RegisterFinishBuilding();
            if (_progressionService.IsBuildingsFinished())
            {
                _locationData.GetData().locationView.ShowBoat();
            }
        }
    }
}