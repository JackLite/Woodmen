using Woodman.Utils;

namespace Woodman.Player.PlayerResources
{
    public sealed class PlayerCoinsRepository : PlayerResRepository
    {
        protected override string SaveKey => "player.resources.coins";

        public PlayerCoinsRepository()
        {
            if (SaveUtility.IsKeyExist(SaveKey))
                count = SaveUtility.LoadInt(SaveKey);
            else 
                count = 1000;
        }
    }
}