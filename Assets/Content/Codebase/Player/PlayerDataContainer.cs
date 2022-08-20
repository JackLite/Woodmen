namespace Woodman.Player
{
    public class PlayerDataContainer
    {
        private readonly PlayerData _playerData;

        public PlayerDataContainer()
        {
            //todo: грузить из сохранки
            _playerData = new PlayerData();
        }

        public int GetMaxLogs()
        {
            return _playerData.maxLogs;
        }
    }
}