using System;
using System.Collections.Generic;

namespace Woodman.MetaTrees
{
    [Serializable]
    public class MetaTreeSaveData
    {
        public readonly Dictionary<string, MetaTree> trees = new();
    }
}