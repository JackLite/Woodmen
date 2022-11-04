using UnityEngine;

namespace Woodman.Meta
{
    [CreateAssetMenu]
    public class VisualSettings : ScriptableObject
    {
        [Header("Transparency")]
        public float speed = 3f;

        [Range(0, 1)]
        public float min = .1f;
    }
}