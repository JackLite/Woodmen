using System.Collections.Generic;
using Woodman.Buildings;
using Woodman.Utils;

namespace Woodman.Locations.Boat
{
    public class BoatSaveService
    {
        private const string SaveKey = "meta.boat.progress";
        private readonly Dictionary<int, BoatSaveData> _boatSaveData;

        public BoatSaveService()
        {
            _boatSaveData = RepositoryHelper.CreateSaveData<Dictionary<int, BoatSaveData>>(SaveKey);
        }

        public bool IsBoatFinished(int locationIndex, BuildingView boat)
        {
            CheckLocation(locationIndex);
            return _boatSaveData[locationIndex].stateIndex >= boat.StatesCount - 1;
        }

        public int GetState(int locationIndex)
        {
            CheckLocation(locationIndex);
            return _boatSaveData[locationIndex].stateIndex;
        }

        public void SetState(int locationIndex, int stateIndex)
        {
            CheckLocation(locationIndex);
            _boatSaveData[locationIndex].stateIndex = stateIndex;
        }

        public int GetLogs(int locationIndex)
        {
            CheckLocation(locationIndex);
            return _boatSaveData[locationIndex].logs;
        }

        public void SetLogs(int locationIndex, int logsCount)
        {
            CheckLocation(locationIndex);
            _boatSaveData[locationIndex].logs = logsCount;
        }

        private void CheckLocation(int locationIndex)
        {
            if (!_boatSaveData.ContainsKey(locationIndex))
                _boatSaveData[locationIndex] = new BoatSaveData { locationIndex = locationIndex };
        }
    }
}