using System;
using Unity.Mathematics;
using Woodman.Utils;

namespace Woodman.Player.PlayerResources
{
    public abstract class PlayerResRepository
    {
        protected int count;

        protected abstract string SaveKey { get; }

        public event Action<int, int> OnChange;

        public int GetPlayerRes()
        {
            return count;
        }

        public void AddPlayerRes(int addCount, bool saveImmediate = true)
        {
            var old = count;
            count += addCount;
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, count);
        }

        public int SubtractRes(int subtractCount, bool saveImmediate = true)
        {
            if (count < subtractCount)
                Logger.LogError(nameof(PlayerResRepository),
                    nameof(SubtractRes),
                    $"Count: {count}. You trying subtract {subtractCount}!");
            var old = count;
            count = math.max(count - subtractCount, 0);
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, count);
            return count;
        }

        private void Save()
        {
            SaveUtility.SaveInt(SaveKey, count, true);
        }
    }
}