using UnityEngine;
using Woodman.Buildings;
using Woodman.Locations.Trees;

namespace Woodman.Locations
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPos;

        [SerializeField]
        [ContextMenuItem(nameof(LoadBuildings), nameof(LoadBuildings))]
        private BuildingView[] _buildings;

        [SerializeField]
        [ContextMenuItem(nameof(LoadTrees), nameof(LoadTrees))]
        private TreeMeta[] _trees;

        [SerializeField]
        private BuildingView _boat;

        private void Awake()
        {
            _boat.gameObject.SetActive(false);
        }

        public Vector3 GetPlayerSpawnPos()
        {
            return _playerSpawnPos.position;
        }

        private void LoadBuildings()
        {
            _buildings = transform.GetComponentsInChildren<BuildingView>(true);
        }

        private void LoadTrees()
        {
            _trees = transform.GetComponentsInChildren<TreeMeta>(true);
        }

        public void SetBuildingsStates(BuildingsRepository buildingsRepository)
        {
            foreach (var view in _buildings)
            {
                var stateIndex = buildingsRepository.GetBuildingStateIndex(view.Id);
                if (!buildingsRepository.IsLastState(view))
                {
                    var current = buildingsRepository.GetBuildingLogsCount(view.Id);
                    var total = view.GetResForState(stateIndex + 1);
                    view.SetLogs(current, total);
                }

                view.SetState(stateIndex);
            }
        }

        public void SetTreesStates(MetaTreesRepository metaTreesRepository)
        {
            foreach (var tree in _trees)
            {
                var isFell = metaTreesRepository.IsFell(tree.Id);
                if (isFell)
                    tree.ShowStump();
                else
                    tree.ShowTree();
            }
        }

        public int GetBuildingsCount()
        {
            return _buildings.Length;
        }

        public void ShowBoat()
        {
            if (_boat != null)
                _boat.gameObject.SetActive(true);
        }

        public void SetBoatState(int index)
        {
            _boat.SetState(index);
        }

        public void SetBoatLogs(int logs, int total)
        {
            _boat.SetLogs(logs, total);
        }
    }
}