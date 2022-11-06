using UnityEngine;
using Woodman.Felling.Tree.Branches;

namespace Woodman.Felling.Tree
{
    public class TreeContainer : MonoBehaviour
    {
        [field: SerializeField]
        public TreePiece TreePrefab { get; private set; }

        [field: SerializeField]
        public BranchView LongBranchPrefab { get; private set; }
    }
}