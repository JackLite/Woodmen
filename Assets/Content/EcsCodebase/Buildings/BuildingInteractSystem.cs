using Core;
using EcsCore;
using Woodman.EcsCodebase.MetaInteractions;
using Woodman.EcsCodebase.MetaInteractions.Components;
using Woodman.EcsCodebase.PlayerRes;

namespace Woodman.EcsCodebase.Buildings
{
    [EcsSystem(typeof(MainModule))]
    public class BuildingInteractSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerResRepository _resRepository;
        private BuildingsRepository _buildingsRepository;
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
            
            Process(buildingId, interact);
        }

        private void Process(string buildingId, BuildingInteract interact)
        {
            var nextState = _buildingsRepository.GetBuildingStateIndex(buildingId) + 1;
            var nextCount = interact.BuildingView.GetResForState(nextState);
            var currentCount = _buildingsRepository.GetBuildingLogsCount(buildingId);
            var playerRes = _resRepository.GetPlayerRes();

            while (playerRes >= nextCount && nextState < 4)
            {
                SetNextState(interact, nextState);
                playerRes = _resRepository.SubtractRes(nextCount);
                nextState++;
                nextCount = interact.BuildingView.GetResForState(nextState);
                currentCount = 0;
            }

            var isLastState = nextState == 4;
            if (isLastState && playerRes > nextCount)
            {
                SetNextState(interact, nextState);
                interact.BuildingView.FinishBuilding();
                //todo: обработка завершения постройки здания
                return;
            }

            interact.BuildingView.AddLogs(playerRes);
            _resRepository.SubtractRes(playerRes);
            _buildingsRepository.SetBuildingLogsCount(currentCount + playerRes, buildingId);
        }

        private void SetNextState(BuildingInteract interact, int nextState)
        {
            interact.BuildingView.SetState(nextState);
            interact.BuildingView.SetLogs(0);
            _buildingsRepository.SetBuildingStateIndex(nextState, interact.BuildingView.Id);
            _buildingsRepository.SetBuildingLogsCount(0, interact.BuildingView.Id);
        }
    }
}