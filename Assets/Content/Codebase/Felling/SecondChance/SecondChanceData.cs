using Woodman.Felling.Finish.Lose;

namespace Woodman.Felling.SecondChance
{
    public struct SecondChanceData
    {
        public bool isActive;
        public bool wasShowed;
        public float remainTime;
        public float totalTime;
        public LoseReason loseReason;
    }
}