using Unity.Mathematics;
using Woodman.Utils;
using Logger = Woodman.Utils.Logger;

namespace Woodman.PlayerRes
{
    public class PlayerResRepository
    {
        private const string SAVE_KEY = "player.resources.logs";

        private int _count;

        public PlayerResRepository()
        {
            // _count = SaveUtility.LoadInt(SAVE_KEY);
            _count  = 100; //todo: temp value
        }

        public int GetPlayerRes()
        {
            return _count;
        }

        public void AddPlayerRes(int count, bool saveImmediate = true)
        {
            _count += count;
            if (saveImmediate)
                Save();
        }

        public void SubtractRes(int count, bool saveImmediate = true)
        {
            if (_count < count)
            {
                Logger.LogError(nameof(PlayerResRepository),
                    nameof(SubtractRes),
                    $"Count: {_count}. You trying subtract ${count}!");
            }
            _count = math.max(_count - count, 0);
            if (saveImmediate)
                Save();
        }

        private void Save()
        {
            SaveUtility.SaveInt(SAVE_KEY, _count, true);
        }
    }
}