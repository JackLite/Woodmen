using UnityEngine;

namespace Woodman.Settings
{
    [CreateAssetMenu]
    public class VisualSettings : ScriptableObject
    {
        [Header("Transparency")]
        public float transparencySpeed = 3f;

        [Range(0, 1)]
        public float min = .1f;

        [Header("Generation")]
        public float gap = 0;
        public float branchX = .4f;
        public float pieceHeight = 1;
        public Vector3 startBias;
        public float branchY = .5f;

        [Header("UsingLogs")]
        public int usingLogsCount = 15;
        public float usingLogsTime = 1f;
        public float usingLogsDelayBetween = .03f;
        public AnimationCurve usingLogsYEasing;
        public float usingLogsYMax = 10;
        public Vector3 usingLogsRotationSpeed = Vector3.zero;

        [Header("Building")]
        public BuildingSettings buildingSettings;

        [Header("Felling")]
        public float fallSpeed = 10f;
    }
}