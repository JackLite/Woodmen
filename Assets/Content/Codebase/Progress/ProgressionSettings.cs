using UnityEngine;

namespace Woodman.Progress
{
    [CreateAssetMenu]
    public class ProgressionSettings : ScriptableObject
    {
        public TreeProgressionInfo[] treeProgressionInfo;

        [Header("Debug")]
        public bool debug;
        public int currentLocationIndex = 1;
        public int currentTreeIndex = 0;
    }
}