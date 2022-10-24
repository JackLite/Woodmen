using System;

namespace Woodman.Felling.Settings
{
    [Serializable]
    public class TreeStrongPieceSettings : TreeElementSettings
    {
        public TreeStrongPieceCountSettings[] countWeight;
    }
}