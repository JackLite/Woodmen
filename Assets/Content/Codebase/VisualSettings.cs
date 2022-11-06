using UnityEngine;

namespace Woodman
{
    [CreateAssetMenu]
    public class VisualSettings : ScriptableObject
    {
        [Header("Transparency")]
        public float speed = 3f;

        [Range(0, 1)]
        public float min = .1f;

        [Header("Generation")]
        public float gap = 0;
        public float branchX = .4f;
        public float pieceHeight = 1;
        public Vector3 startBias;
        public float branchY = .5f;
    }
}