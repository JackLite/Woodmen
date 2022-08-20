using Unity.Mathematics;
using Woodman.MetaInteractions;
using Woodman.Player;
using Woodman.PlayerRes;

namespace Woodman.Logs
{
    public class LogsInteraction
    {
        private readonly PlayerResRepository _resRepository;
        private readonly PlayerDataContainer _playerData;

        public LogsInteraction(PlayerResRepository resRepository, PlayerDataContainer playerData)
        {
            _resRepository = resRepository;
            _playerData = playerData;
        }

        public void OnInteract(InteractTarget target)
        {
            var logInteract = target as LogInteract;
            if (logInteract != null && logInteract.LogView.Count > 0)
            {
                var currentRes = _resRepository.GetPlayerRes();
                if (currentRes >= _playerData.GetMaxLogs())
                    return;

                var toAdd = math.min(_playerData.GetMaxLogs() - currentRes, logInteract.LogView.Count);
                _resRepository.AddPlayerRes(toAdd);
                logInteract.LogView.Subtract(toAdd);
            }
        }
    }
}