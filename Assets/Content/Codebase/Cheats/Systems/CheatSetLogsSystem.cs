using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Unity.Mathematics;
using Woodman.Cheats.View;
using Woodman.Player.PlayerResources;

namespace Woodman.Cheats.Systems
{
    [EcsSystem(typeof(CheatsModule))]
    public class CheatSetLogsSystem : IInitSystem, IDestroySystem
    {
        private DebugResourceViewProvider _viewProvider;
        private PlayerLogsRepository _playerResRepository;
        private DebugViewProvider _debugViewProvider;
        public void Init()
        {
            _viewProvider.LogsApplyBtn.onClick.AddListener(ApplyLogs);
        }

        private void ApplyLogs()
        {
            if(int.TryParse(_viewProvider.LogsInput.text, out var count))
            {
                var current = _playerResRepository.GetPlayerRes();
                _playerResRepository.SubtractRes(current);
                _playerResRepository.AddPlayerRes(math.max(0, count));
                _debugViewProvider.DebugMessageView.SetMsg("Success set " + count + " logs");
            }
            else
            {
                _debugViewProvider.DebugMessageView.SetMsg("Wrong format!");
            }
        }

        public void Destroy()
        {
            _viewProvider.LogsApplyBtn.onClick.RemoveListener(ApplyLogs);
        }
    }
}