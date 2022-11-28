using System;

namespace Woodman.Settings
{
    [Serializable]
    public class BuildingSettings
    {
        public float transparencyDownTime = 0.6f;
        public float transparencyUpTime = 0.6f;
        public float transparencyValue = 2;
        public float delay;
        public float blinkTime = 1f;
        public float blinkValue = 1;
    }
}