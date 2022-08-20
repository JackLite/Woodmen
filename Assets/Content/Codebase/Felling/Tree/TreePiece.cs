using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiece : MonoBehaviour
    {
        [field: SerializeField]
        public Transform TreeMesh { get; private set; }

        public FellingSide FellingSide { get; set; }
        public bool IsHasBranch { get; set; }
    }
}