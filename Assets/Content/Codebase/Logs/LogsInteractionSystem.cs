using Core;
using EcsCore;
using Unity.Mathematics;
using Woodman.Logs;
using Woodman.MetaInteractions.Components;
using Woodman.Player;
using Woodman.PlayerRes;

namespace Woodman.MetaInteractions.LogsInteraction
{
    [EcsSystem(typeof(MainModule))]
    public class LogsInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<PlayerOneData> _playerData;
        private LogsHeapRepository _logsHeapRepository;
        private PlayerResRepository _resRepository;

        public void Run()
        {
            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Logs);

            if (!q.TrySelectFirst(out Interact interact))
                return;

            var logs = interact.target as LogInteract;
            if (!logs)
                return;
            if (logs.LogView.Count <= 0)
                return;
            
            
            var currentRes = _resRepository.GetPlayerRes();
            var max = _playerData.GetData().maxWoodCount;
            if (currentRes >= max)
                return;

            var toAdd = math.min(max - currentRes, logs.LogView.Count);
            _resRepository.AddPlayerRes(toAdd);
            logs.LogView.Subtract(toAdd);
            _logsHeapRepository.SetCount(logs.LogView.Id, logs.LogView.Count);
        }
    }
}