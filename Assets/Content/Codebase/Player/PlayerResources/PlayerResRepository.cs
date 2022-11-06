using System;
using Unity.Mathematics;
using Woodman.Utils;

namespace Woodman.Player.PlayerResources
{
    public class PlayerResRepository
    {
        private const string SAVE_KEY = "player.resources.logs";

        private int _count;

        public PlayerResRepository()
        {
            _count = SaveUtility.LoadInt(SAVE_KEY);
        }

        public event Action<int, int> OnChange;

        public int GetPlayerRes()
        {
            return _count;
        }

        public void AddPlayerRes(int count, bool saveImmediate = true)
        {
            var old = _count;
            _count += count;
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, _count);
        }

        public int SubtractRes(int count, bool saveImmediate = true)
        {
            if (_count < count)
                Logger.LogError(nameof(PlayerResRepository),
                    nameof(SubtractRes),
                    $"Count: {_count}. You trying subtract ${count}!");
            var old = _count;
            _count = math.max(_count - count, 0);
            if (saveImmediate)
                Save();
            OnChange?.Invoke(old, _count);
            return _count;
        }

        private void Save()
        {
            SaveUtility.SaveInt(SAVE_KEY, _count, true);
        }
    }
}