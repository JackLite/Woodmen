using System;

namespace Woodman.Felling.Settings
{
    [Serializable]
    public struct TreeGenerationSettings
    {
        public TreeElementSettings hollow;
        public TreeStrongPieceSettings strong;
        public TreeElementSettings timeFreeze;
        public TreeElementSettings restoreTime;
        public TreeElementSettings hive;
    }
}