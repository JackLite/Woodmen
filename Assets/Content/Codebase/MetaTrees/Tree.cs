using UnityEngine;

namespace Woodman.MetaTrees
{
    public class Tree : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerTargetPosition;

        public Vector3 PlayerWorldPosition => _playerTargetPosition.position;
    }
}