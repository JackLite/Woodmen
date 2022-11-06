using System;
using System.Collections.Generic;

namespace Woodman.Locations.Trees
{
    [Serializable]
    public class MetaTreeSaveData
    {
        public readonly Dictionary<string, MetaTree> trees = new();
    }
}