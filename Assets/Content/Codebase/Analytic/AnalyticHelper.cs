using System;
using System.Collections.Generic;
using Woodman.Felling.Finish;
using Woodman.Felling.Finish.Lose;
using Woodman.Felling.Tree.Progression;
using Woodman.Utils;

namespace Woodman.Analytic
{
    public static class AnalyticHelper
    {
        private static readonly Dictionary<TreeDifficult, string> TreeDiffMap = new()
        {
            { TreeDifficult.Easy, "easy" },
            { TreeDifficult.Moderate, "moderate" },
            { TreeDifficult.Hard, "hard" }
        };

        private static readonly Dictionary<FellingFinishReason, string> FinishReasonMap = new()
        {
            { FellingFinishReason.Win, "win" },
            { FellingFinishReason.Lose, "lose" },
            { FellingFinishReason.Restart, "restart" },
            { FellingFinishReason.GameClosed, "game_closed" },
        };
        
        private static readonly Dictionary<LoseReason, string> LoseReasonMap = new()
        {
            { LoseReason.BranchCollide, "branch" },
            { LoseReason.HiveCollide, "hive" },
            { LoseReason.TimeOut, "timeout" },
        };

        private const string LevelCountKey = "analytic.felling.lifetime_count";
        private const string LevelStartedKey = "analytic.felling.is_started";
        private static DateTime _startFellingTime;

        public static string GetDiff(TreeDifficult difficult)
        {
            if (TreeDiffMap.TryGetValue(difficult, out var diffString))
                return diffString;
            return "unknown_difficult";
        }

        public static string GetFinishReason(FellingFinishReason reason)
        {
            if (FinishReasonMap.TryGetValue(reason, out var reasonString))
                return reasonString;
            return "unknown_finish_reason";
        }

        public static void RegisterStartFelling()
        {
            SaveUtility.SaveBool(LevelStartedKey, true);
            var count = 0;
            if (SaveUtility.IsKeyExist(LevelCountKey))
                count = SaveUtility.LoadInt(LevelCountKey);
            count++;
            SaveUtility.SaveInt(LevelCountKey, count, true);
            _startFellingTime = DateTime.Now;
        }

        public static int GetLevelCount()
        {
            if (SaveUtility.IsKeyExist(LevelCountKey))
                return SaveUtility.LoadInt(LevelCountKey);
            return 1;
        }

        public static bool IsLevelStarted()
        {
            return SaveUtility.LoadBool(LevelStartedKey);
        }

        public static void RegisterFinishFelling()
        {
            SaveUtility.SaveBool(LevelStartedKey, false, true);
        }

        public static int GetFellingTime()
        {
            var diff = DateTime.Now - _startFellingTime;
            return (int) diff.TotalSeconds;
        }

        public static string GetLoseReason(LoseReason reason)
        {
            if (LoseReasonMap.TryGetValue(reason, out var reasonString))
                return reasonString;
            return "unknown_lose_reason";
        }
    }
}