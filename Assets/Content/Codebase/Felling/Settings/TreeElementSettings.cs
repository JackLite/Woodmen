using System;

namespace Woodman.Felling.Settings
{
    [Serializable]
    public class TreeElementSettings
    {
        public int afterIndex;
        public int afterTree;
        public float possibilityCoef;
        public float startPossibility;
        public float maxPossibility;
    }
}