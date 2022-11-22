using System;

namespace Woodman.Tutorial
{
    [Serializable]
    public struct TutorialData
    {
        public bool firstStepComplete;
        public bool secondStepComplete;
        public bool thirdStepComplete;
        public bool tutorialComplete;

        public bool isDirty;
    }
}