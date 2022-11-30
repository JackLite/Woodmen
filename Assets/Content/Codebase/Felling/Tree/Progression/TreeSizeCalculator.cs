using System.Collections.Generic;
using System.Linq;
using Woodman.Progress;

namespace Woodman.Felling.Tree.Progression
{
    public static class TreeSizeCalculator
    {
        public static TreeSize CalculateTreeSize(
            LastTreeResults results,
            TreeProgressionInfo treeProgressionInfo,
            Dictionary<TreeDifficult, int> treesProgress)
        {
            var hardEasyMod = new[] { TreeDifficult.Hard, TreeDifficult.Easy, TreeDifficult.Moderate };
            var hardModEasy = new[] { TreeDifficult.Hard, TreeDifficult.Easy, TreeDifficult.Moderate };
            var modEasyHard = new[] { TreeDifficult.Moderate, TreeDifficult.Easy, TreeDifficult.Hard };
            var modHardEasy = new[] { TreeDifficult.Moderate, TreeDifficult.Hard, TreeDifficult.Easy };
            var easyModHard = new[] { TreeDifficult.Easy, TreeDifficult.Moderate, TreeDifficult.Hard };

            if (results.isWin && results.difficult == TreeDifficult.Hard)
                return GetSize(hardEasyMod, treeProgressionInfo, treesProgress);
            if (!results.isWin && results.difficult == TreeDifficult.Hard)
            {
                if (results.count <= 1)
                    return GetSize(modEasyHard, treeProgressionInfo, treesProgress);
                return GetSize(hardModEasy, treeProgressionInfo, treesProgress);
            }

            if (results.isWin && results.difficult == TreeDifficult.Moderate)
            {
                if (results.count <= 1)
                    return GetSize(modHardEasy, treeProgressionInfo, treesProgress);
                return GetSize(hardModEasy, treeProgressionInfo, treesProgress);
            }

            if (!results.isWin && results.difficult == TreeDifficult.Moderate)
            {
                if (results.count <= 1)
                    return GetSize(easyModHard, treeProgressionInfo, treesProgress);
                return GetSize(modEasyHard, treeProgressionInfo, treesProgress);
            }

            if (results.isWin && results.difficult == TreeDifficult.Easy)
            {
                if (results.count <= 1)
                    return GetSize(easyModHard, treeProgressionInfo, treesProgress);
                return GetSize(modHardEasy, treeProgressionInfo, treesProgress);
            }

            return GetSize(easyModHard, treeProgressionInfo, treesProgress);
        }

        private static TreeSize GetSize(TreeDifficult[] treeDifficultOrder, TreeProgressionInfo treeProgressionInfo,
            Dictionary<TreeDifficult, int> treesProgress)
        {
            foreach (var difficult in treeDifficultOrder)
            {
                var index = treesProgress[difficult];
                if (difficult == TreeDifficult.Hard && index < treeProgressionInfo.hardTrees.Length)
                {
                    return new TreeSize
                    {
                        size = treeProgressionInfo.hardTrees[index],
                        difficult = TreeDifficult.Hard
                    };
                }

                if (difficult == TreeDifficult.Moderate && index < treeProgressionInfo.middleTrees.Length)
                {
                    return new TreeSize
                    {
                        size = treeProgressionInfo.middleTrees[index],
                        difficult = TreeDifficult.Moderate
                    };
                }

                if (difficult == TreeDifficult.Easy && index < treeProgressionInfo.easyTrees.Length)
                {
                    return new TreeSize
                    {
                        size = treeProgressionInfo.easyTrees[index],
                        difficult = TreeDifficult.Easy
                    };
                }
            }

            return new TreeSize
            {
                size = treeProgressionInfo.easyTrees.Last(),
                difficult = TreeDifficult.Easy
            };
        }
    }
}