using UnityEngine;
using Woodman.Felling;

namespace Woodman.MetaTrees
{
    public class TreeMeta : MonoBehaviour
    {
        [SerializeField]
        private Transform _leftCutPosition;

        [SerializeField]
        private Transform _rightCutPosition;

        [SerializeField]
        private GameObject _mesh;
        
        public Vector3 GetCutPosition(Side side)
        {
            return side == Side.Right ? _rightCutPosition.position : _leftCutPosition.position;
        }

        public void HideMesh()
        {
            _mesh.SetActive(false);
        }
    }
}