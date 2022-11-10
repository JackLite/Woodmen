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

        public void AddPlayerRes(int count, bool saveImmediate = true)
        {
            var old = this.count;
            this.count += count;
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, this.count);
        }

        public int SubtractRes(int count, bool saveImmediate = true)
        {
            if (this.count < count)
                Logger.LogError(nameof(PlayerResRepository),
                    nameof(SubtractRes),
                    $"Count: {this.count}. You trying subtract ${count}!");
            var old = this.count;
            this.count = math.max(this.count - count, 0);
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, this.count);
            return this.count;
        }

        private void Save()
        {
            SaveUtility.SaveInt(SaveKey, count, true);
        }
    }
}