using System;
using System.Collections.Generic;

namespace Woodman.Felling.Tree.Progression
{
    [Serializable]
    public class TreeProgressionSaveData
    {
        public LastTreeResults lastResult;
        public TreeDifficult lastDifficult;

        public readonly Dictionary<TreeDifficult, int> treesProgress = new()
        {
            { TreeDifficult.Easy, 0 },
            { TreeDifficult.Moderate, 0 },
            { TreeDifficult.Hard, 0 },
        };
    }
}