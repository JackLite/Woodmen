using UnityEngine;

namespace Woodman.Locations
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawnPos;

        public Vector3 GetPlayerSpawnPos()
        {
            return _playerSpawnPos.position;
        }
    }
}