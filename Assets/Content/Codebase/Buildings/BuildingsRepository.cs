using Woodman.Utils;

namespace Woodman.Buildings
{
    /// <summary>
    /// Данные по домикам
    /// </summary>
    public class BuildingsRepository
    {
        private const string BuildingsSaveKey = "meta.buildings.progress";
        private readonly BuildingSave _buildingSave;

        public BuildingsRepository()
        {
            _buildingSave = RepositoryHelper.CreateSaveData<BuildingSave>(BuildingsSaveKey);
        }
        
        public int GetBuildingStateIndex(string building)
        {
            CheckBuilding(building);
            return _buildingSave.building[building].state;
        }
        
        public int GetBuildingLogsCount(string building)
        {
            CheckBuilding(building);
            return _buildingSave.building[building].logsCount;
        }

        public void SetBuildingStateIndex(int index, string building)
        {
            CheckBuilding(building);
            _buildingSave.building[building].state = index;
            Save();
        }
        
        public void SetBuildingLogsCount(int count,string building)
        {
            CheckBuilding(building);
            _buildingSave.building[building].logsCount = count;
            Save();
        }

        public bool IsLastState(BuildingView building)
        {
            return GetBuildingStateIndex(building.Id) >= building.StatesCount - 1;
        }

        private void CheckBuilding(string building)
        {
            if (!_buildingSave.building.ContainsKey(building))
            {
                _buildingSave.building[building] = new BuildingData
                {
                    id = building,
                    state = 0,
                    logsCount = 0
                };
            }
        }

        private void Save()
        {
            RepositoryHelper.Save(BuildingsSaveKey, _buildingSave);
        }
    }
}