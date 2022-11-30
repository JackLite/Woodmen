using System.Collections.Generic;
using NUnit.Framework;
using Woodman.Felling.Tree.Progression;
using Woodman.Progress;

namespace Tests
{
    public class ProgressionTests
    {
        [Test]
        public void TestWinFirstTreeEasy_ReturnEasy()
        {
            var lastResult = new LastTreeResults { count = 1, difficult = TreeDifficult.Easy, isWin = true };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(2, size.size);
        }
    
        [Test]
        public void TestWinSecondTreeEasy_ReturnModerate()
        {
            var lastResult = new LastTreeResults { count = 2, difficult = TreeDifficult.Easy, isWin = true };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(4, size.size);
        }
    
        [Test]
        public void TestWinFirstTreeModerate_ReturnModerate()
        {
            var lastResult = new LastTreeResults { count = 1, difficult = TreeDifficult.Moderate, isWin = true };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 1 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(5, size.size);
        }
    
        [Test]
        public void TestWinSecondTreeModerate_ReturnHard()
        {
            var lastResult = new LastTreeResults { count = 25, difficult = TreeDifficult.Moderate, isWin = true };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(7, size.size);
        }
    
        [Test]
        public void TestWinTreeHard_ReturnHard([Values(0, 1, 2, 3)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Hard, isWin = true };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.GreaterOrEqual(7, size.size);
        }

        [Test]
        public void TestLoseTreeEasy_ReturnEasy([Values(0, 1, 2, 3)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Easy, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(2, size.size);
        }
    
        [Test]
        public void TestLoseFirstTreeModerate_ReturnEasy([Values(0, 1)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Moderate, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(2, size.size);
        }
    
        [Test]
        public void TestLoseNotFirstTreeModerate_ReturnModerate([Values(2, 3)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Moderate, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(4, size.size);
        }
    
        [Test]
        public void TestLoseFirstTreeHard_ReturnModerate([Values(0, 1)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Hard, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(4, size.size);
        }
    
        [Test]
        public void TestLoseNotFirstTreeHard_ReturnHard([Values(2, 3)] int count)
        {
            var lastResult = new LastTreeResults { count = count, difficult = TreeDifficult.Hard, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 0 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(7, size.size);
        }
    
        [Test]
        public void TestWinHardNoHardLeft_ReturnEasy()
        {
            var lastResult = new LastTreeResults { count = 2, difficult = TreeDifficult.Hard, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 0 },
                { TreeDifficult.Hard, 3 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(2, size.size);
        }
    
        [Test]
        public void TestWinModerateNoModerateAndHardLeft_ReturnEasy()
        {
            var lastResult = new LastTreeResults { count = 2, difficult = TreeDifficult.Moderate, isWin = false };
            var info = new TreeProgressionInfo
            {
                easyTrees = new[] { 1, 2, 3 },
                middleTrees = new[] { 4, 5, 6 },
                hardTrees = new[] { 7, 8, 9 },
            };
            var progress = new Dictionary<TreeDifficult, int>
            {
                { TreeDifficult.Easy, 1 },
                { TreeDifficult.Moderate, 3 },
                { TreeDifficult.Hard, 3 },
            };

            var size = TreeSizeCalculator.CalculateTreeSize(lastResult, info, progress);

            Assert.AreEqual(2, size.size);
        }
    }
}
