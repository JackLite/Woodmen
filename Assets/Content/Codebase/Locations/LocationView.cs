using System.Collections.Generic;
using UnityEngine;
using Woodman.Buildings;
using Woodman.Locations.Trees;
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

        [SerializeField]
        [ContextMenuItem(nameof(LoadTrees), nameof(LoadTrees))]
        private TreeMeta[] _trees;

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
                if (!buildingsRepository.IsLastState(view.Id))
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
                    tree.DisableMeta();
                else
                    tree.EnableMeta();
            }
        }
    }
}