using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreeContainer : MonoBehaviour
    {
        [field: SerializeField]
        public TreePiece TreePrefab { get; private set; }

        [field: SerializeField]
        public GameObject LongBenchPrefab { get; private set; }

        [field: SerializeField]
        public GameObject ShortBenchPrefab { get; private set; }
    }
}