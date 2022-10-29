using System;

namespace Woodman.Felling.Settings
{
    [Serializable]
    public class TreeGenerationSettings
    {
        public BranchSwitchingSettings branchSwitching;
        public TreeElementSettings hollow;
        public TreeStrongPieceSettings strong;
        public TreeElementSettings timeFreeze;
        public TreeElementSettings restoreTime;
        public TreeElementSettings hive;
    }
}