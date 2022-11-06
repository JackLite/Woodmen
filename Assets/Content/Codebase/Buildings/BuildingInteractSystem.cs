using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Player.PlayerResources;

namespace Woodman.Buildings
{
    [EcsSystem(typeof(MetaModule))]
    public class BuildingInteractSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerResRepository _resRepository;
        private BuildingsRepository _buildingsRepository;
        private PoolsProvider _poolsProvider;

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

            var buildingId = interact.BuildingView.Id;
            if (_buildingsRepository.IsLastState(buildingId))
                return;

            Process(interact);
        }

        private void Process(BuildingInteract interact)
        {
            var delta = CalcNextState(interact.BuildingView);
            var oldState = _buildingsRepository.GetBuildingStateIndex(interact.BuildingView.Id);
            _resRepository.SubtractRes(delta.totalResources);
            _buildingsRepository.SetBuildingStateIndex(delta.resultState, interact.BuildingView.Id);
            _buildingsRepository.SetBuildingLogsCount(delta.resultLogsCount, interact.BuildingView.Id);

            interact.BuildingView.SetLogs(delta.resultLogsCount, delta.nextStateLogsCount);

            if (oldState != delta.resultState)
            {
                interact.BuildingView.AnimateTo(delta.resultState, _poolsProvider.BuildingFxPool);
            }
        }

        private BuildingDelta CalcNextState(BuildingView building)
        {
            var delta = new BuildingDelta
            {
                resultState = _buildingsRepository.GetBuildingStateIndex(building.Id)
            };

            var currentCount = _buildingsRepository.GetBuildingLogsCount(building.Id);
            var nextCount = building.GetResForState(delta.resultState + 1) - currentCount;
            var playerRes = _resRepository.GetPlayerRes();
            delta.resultLogsCount = currentCount;
            while (playerRes >= nextCount)
            {
                playerRes -= nextCount;
                delta.resultState++;
                if (delta.resultState < 4)
                {
                    nextCount = building.GetResForState(delta.resultState + 1);
                    delta.resultLogsCount = 0;
                    delta.nextStateLogsCount = nextCount;
                }
                else
                {
                    delta.resultLogsCount = 0;
                    delta.nextStateLogsCount = 0;
                    break;
                }
            }

            if (delta.resultState < 4)
            {
                delta.resultLogsCount += playerRes;
                delta.nextStateLogsCount = building.GetResForState(delta.resultState + 1);
                playerRes = 0;
            }

            delta.totalResources = _resRepository.GetPlayerRes() - playerRes;
            return delta;
        }

        private struct BuildingDelta
        {
            public int totalResources;
            public int resultState;
            public int resultLogsCount;
            public int nextStateLogsCount;
        }
    }
}