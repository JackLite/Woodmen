using System.Collections.Generic;

namespace Woodman.EcsCodebase.Buildings
{
    /// <summary>
    /// Данные по домикам
    /// </summary>
    public class BuildingsRepository
    {
        private readonly Dictionary<string, BuildingData> _building = new();

        public int GetBuildingStateIndex(string building)
        {
            CheckBuilding(building);
            return _building[building].state;
        }
        
        public int GetBuildingLogsCount(string building)
        {
            CheckBuilding(building);
            return _building[building].logsCount;
        }

        public void SetBuildingStateIndex(int index, string building)
        {
            CheckBuilding(building);
            _building[building].state = index;
        }
        
        public void SetBuildingLogsCount(int count,string building)
        {
            CheckBuilding(building);
            _building[building].logsCount = count;
        }

        public bool IsLastState(string building)
        {
            return GetBuildingStateIndex(building) == 4;
        }

        private void CheckBuilding(string building)
        {
            if (!_building.ContainsKey(building))
            {
                _building[building] = new BuildingData
                {
                    id = building,
                    state = 0,
                    logsCount = 0
                };
            }
        }
    }
}