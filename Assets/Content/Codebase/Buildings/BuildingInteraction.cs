using UnityEngine;
using Woodman.MetaInteractions;
using Woodman.PlayerRes;

namespace Woodman.Buildings
{
    public class BuildingInteraction
    {
        private readonly PlayerResRepository _resRepository;
        private readonly BuildingsRepository _buildingsRepository;

        public BuildingInteraction(PlayerResRepository resRepository, BuildingsRepository buildingsRepository)
        {
            _resRepository = resRepository;
            _buildingsRepository = buildingsRepository;
        }

        public void OnInteract(InteractTarget target)
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

            while (playerRes > nextCount && !_buildingsRepository.IsLastState(buildingId))
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