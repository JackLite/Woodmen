using Woodman.Locations.Boat;
using Woodman.Player.PlayerResources;

namespace Woodman.Buildings
{
    public class BuildingService
    {
        private readonly BuildingsRepository _buildingsRepository;
        private readonly BoatSaveService _boatSaveService;
        private readonly PlayerLogsRepository _resRepository;

        public BuildingService(
            BuildingsRepository buildingsRepository,
            PlayerLogsRepository resRepository,
            BoatSaveService boatSaveService)
        {
            _buildingsRepository = buildingsRepository;
            _resRepository = resRepository;
            _boatSaveService = boatSaveService;
        }

        public BuildingDelta CalcHouseNextState(BuildingView building)
        {
            var currentState = _buildingsRepository.GetBuildingStateIndex(building.Id);
            var currentLogs = _buildingsRepository.GetBuildingLogsCount(building.Id);
            return CalcNextState(building, currentState, currentLogs);
        }

        public BuildingDelta CalcBoatNextState(int locationIndex, BuildingView building)
        {
            var currentState = _boatSaveService.GetState(locationIndex);
            var currentLogs = _boatSaveService.GetLogs(locationIndex);
            return CalcNextState(building, currentState, currentLogs);
        }

        private BuildingDelta CalcNextState(BuildingView building, int currentState, int currentLogs)
        {
            var delta = new BuildingDelta
            {
                resultState = currentState,
                resultLogsCount = currentLogs
            };

            var nextCount = building.GetResForState(delta.resultState + 1) - currentLogs;
            var playerRes = _resRepository.GetPlayerRes();
            while (playerRes >= nextCount)
            {
                playerRes -= nextCount;
                delta.resultState++;
                if (delta.resultState < building.StatesCount - 1)
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

            if (delta.resultState < building.StatesCount - 1)
            {
                delta.resultLogsCount += playerRes;
                delta.nextStateLogsCount = building.GetResForState(delta.resultState + 1);
                playerRes = 0;
            }

            delta.totalResources = _resRepository.GetPlayerRes() - playerRes;
            return delta;
        }
    }
}