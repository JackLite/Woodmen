using UnityEngine;
using Woodman.Felling.Tree;

namespace Woodman.MetaTrees
{
    public class TreeMeta : MonoBehaviour
    {
        [SerializeField]
        private int _size = 100;

        [SerializeField]
        private Transform _leftCutPosition;

        [SerializeField]
        private Transform _rightCutPosition;

        [SerializeField]
        private GameObject _mesh;


        public void HideMesh()
        {
            _mesh.SetActive(false);
        }

        public TreeModel GetTreeModel()
        {
            return new TreeModel
            {
                pos = transform.position,
                leftPos = _leftCutPosition.position,
                rightPos = _rightCutPosition.position,
                size = _size
            };
        }
    }
}