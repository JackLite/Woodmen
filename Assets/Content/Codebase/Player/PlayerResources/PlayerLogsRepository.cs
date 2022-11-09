using Woodman.Utils;

namespace Woodman.Player.PlayerResources
{
    public sealed class PlayerLogsRepository : PlayerResRepository
    {
        protected override string SaveKey => "player.resources.logs";

        public PlayerLogsRepository()
        {
            count = SaveUtility.LoadInt(SaveKey);
        }
    }
}