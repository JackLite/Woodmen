using Woodman.Felling.Finish.Lose;

namespace Woodman.Felling.Finish
{
    public struct FellingFinishSignal
    {
        public FellingFinishReason reason;
        public LoseReason loseReason;
        public float progress;
        public bool secondChanceShowed;
    }
}