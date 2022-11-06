using UnityEngine;

namespace Woodman.Felling
{
    /// <summary>
    /// Container for positions of tree's root and character (left and right)
    /// </summary>
    public class FellingPositions : MonoBehaviour
    {
        [SerializeField]
        private Transform _treeRoot;

        [SerializeField]
        private Transform _leftCharPos;
        
        [SerializeField]
        private Transform _rightCharPos;

        public Vector3 RootPos => _treeRoot.position;
        public Vector3 LeftCharPos => _leftCharPos.position;
        public Vector3 RightCharPos => _rightCharPos.position;
    }
}