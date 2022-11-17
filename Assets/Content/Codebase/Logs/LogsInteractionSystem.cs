using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using Woodman.Common.Tweens;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;
using Woodman.Logs.LogsUsing;
using Woodman.Player;
using Woodman.Player.PlayerResources;

namespace Woodman.Logs
{
    [EcsSystem(typeof(MetaModule))]
    public class LogsInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<PlayerData> _playerData;
        private CharacterLogsView _characterLogsView;
        private LogsHeapRepository _logsHeapRepository;
        private LogsPool _logsPool;
        private PlayerLogsRepository _resRepository;
        private VisualSettings _visualSettings;

        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Logs);

            if (!q.TrySelectFirst(out Interact interact))
                return;

            var logs = interact.target as LogInteract;
            if (!logs)
                return;
            var currentLogs = logs.LogView.Count;
            if (currentLogs <= 0)
                return;

            var currentRes = _resRepository.GetPlayerRes();
            var max = _playerData.GetData().maxWoodCount;
            if (currentRes >= max)
                return;

            var toAdd = math.min(max - currentRes, currentLogs);
            _resRepository.AddPlayerRes(toAdd);
            var endLogs = currentLogs - toAdd;
            _logsHeapRepository.SetCount(logs.LogView.Id, endLogs);

            var createEvent = new UsingLogsCreateEvent
            {
                count = _visualSettings.usingLogsCount,
                from = logs.LogView.UsingPoint,
                to = () => _characterLogsView.LogsTargetPos,
                delayBetween = _visualSettings.usingLogsDelayBetween,
                onAfter = () =>
                {
                    if (endLogs <= 0)
                        _logsPool.Return(logs.LogView);
                }
            };
            _world.NewEntity().AddComponent(createEvent);
            
            var totalTime = _visualSettings.usingLogsTime +
                            _visualSettings.usingLogsCount * _visualSettings.usingLogsDelayBetween;
            var tweenData = new TweenData
            {
                remain = totalTime,
                update = r =>
                {
                    var logsCount = (int)math.lerp(currentLogs, endLogs, 1 - r / totalTime);
                    logs.LogView.SetCount(logsCount);
                },
                validate = () => logs.LogView != null,
                onEnd = () => logs.LogView.SetCount(endLogs)
            };
            _world.NewEntity().AddComponent(tweenData);
        }
    }
}