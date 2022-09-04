using System.Collections.Generic;
using UnityEngine;
using Woodman.Buildings;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Locations
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPos;

        [SerializeField]
        [ContextMenuItem(nameof(LoadBuildings), nameof(LoadBuildings))]
        private BuildingView[] _buildings;

        public Vector3 GetPlayerSpawnPos()
        {
            return _playerSpawnPos.position;
        }

        private void LoadBuildings()
        {
            _buildings = transform.GetComponentsInChildren<BuildingView>(true);
        }

        public void SetBuildingsStates(BuildingsRepository buildingsRepository)
        {
            foreach (var view in _buildings)
            {
                view.SetLogs(buildingsRepository.GetBuildingLogsCount(view.Id));
                view.SetState(buildingsRepository.GetBuildingStateIndex(view.Id));
            }
        }
    }
}