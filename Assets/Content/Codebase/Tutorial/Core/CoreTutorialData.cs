using System;

namespace Woodman.Tutorial.Core
{
    [Serializable]
    public struct CoreTutorialData
    {
        public bool rightTapComplete;
        public bool leftTapComplete;
        public bool branchComplete;
        public bool timerComplete;
        public bool progressComplete;
        public bool freezeComplete;
        public bool refillComplete;
        public bool hiveComplete;

        public bool baseComplete;
        public bool isDirty;
    }
}