using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Player.PlayerResources;
using Woodman.Progress;

namespace Woodman.Locations.Boat
{
    [EcsSystem(typeof(MetaModule))]
    public class BoatInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerLogsRepository _resRepository;
        private BoatSaveService _boatSaveService;
        private PoolsProvider _poolsProvider;
        private ProgressionService _progressionService;
        private BuildingService _buildingService;
        private EcsOneData<LocationData> _locationData;
        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(c => c.interactType == InteractTypeEnum.Boat);
            if (!q.TrySelectFirst(out Interact interact))
                return;

            OnInteract(interact.target);
        }

        private void OnInteract(InteractTarget target)
        {
            var interact = target.GetComponent<BuildingInteract>();
            if (interact == null)
                return;

            if (_boatSaveService.IsBoatFinished(_progressionService.GetLocationIndex(), interact.BuildingView))
                return;

            Process(interact);
        }

        private void Process(BuildingInteract interact)
        {
            var locationIndex = _progressionService.GetLocationIndex();
            var delta = _buildingService.CalcBoatNextState(locationIndex, interact.BuildingView);
            var oldState = _boatSaveService.GetState(locationIndex);
            _resRepository.SubtractRes(delta.totalResources);
            _boatSaveService.SetState(locationIndex, delta.resultState);
            _boatSaveService.SetLogs(locationIndex, delta.resultLogsCount);

            interact.BuildingView.SetLogs(delta.resultLogsCount, delta.nextStateLogsCount);

            if (oldState != delta.resultState)
            {
                interact.BuildingView.AnimateTo(delta.resultState, _poolsProvider.BuildingFxPool);
                if (delta.resultState == interact.BuildingView.StatesCount - 1)
                    FinishLocation();
            }
        }

        private void FinishLocation()
        {
            _world.NewEntity().AddComponent(new NextLocationEvent());
        }
    }
}