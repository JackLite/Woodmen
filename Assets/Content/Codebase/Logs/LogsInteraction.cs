using Woodman.MetaInteractions;
using Woodman.PlayerRes;

namespace Woodman.Logs
{
    public class LogsInteraction
    {
        private readonly PlayerResRepository _resRepository;

        public LogsInteraction(PlayerResRepository resRepository)
        {
            _resRepository = resRepository;
        }

        public void OnInteract(InteractTarget target)
        {
            var logInteract = target as LogInteract;
            if (logInteract != null && logInteract.LogView.Count > 0)
            {
                _resRepository.AddPlayerRes(logInteract.LogView.Count);
                logInteract.LogView.Count = 0;
                logInteract.LogView.Hide();
            }
        }
    }
}