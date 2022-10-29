using System;

namespace Woodman.Felling.Settings
{
    /// <summary>
    /// Switch possibility:
    /// currentAcc = clamp(minAcc, maxAcc, currentAcc + accDelta * (accMultiplier * (pieceIndex / accMod))
    /// SP = clamp(min, max, current + currentAcc)
    /// </summary>
    [Serializable]
    public class BranchSwitchingSettings
    {
        public float min;
        public float max;
        public float minAcc;
        public float maxAcc;
        public float accDelta;
        public float accMultiplier;
        public int accMod;
    }
}