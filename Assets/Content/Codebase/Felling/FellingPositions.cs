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

        public Vector3 RootPos => _treeRoot.position;
    }
}