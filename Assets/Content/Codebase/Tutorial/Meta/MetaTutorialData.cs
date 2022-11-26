using System;

namespace Woodman.Tutorial.Meta
{
    [Serializable]
    public struct MetaTutorialData
    {
        public bool firstStepComplete;
        public bool secondStepComplete;
        public bool thirdStepComplete;
        public bool tutorialComplete;

        public bool isDirty;
    }
}