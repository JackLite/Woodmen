using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Player.PlayerResources;

namespace Woodman.Player.Indicators
{
    [EcsSystem(typeof(MetaModule))]
    public class CharacterUpdateBackpackSystem : IInitSystem, IPostRunSystem
    {
        private PlayerIndicatorsController _indicatorsController;
        private PlayerLogsRepository _logsRepository;
        private DataWorld _world;

        public void Init()
        {
            UpdateLogs();
        }

        private void UpdateLogs()
        {
            _indicatorsController.ToggleLogBackpack(_logsRepository.GetPlayerRes() > 0);
        }

        public void PostRun()
        {
            if (!_world.Select<ChangeResEvent>().Any())
                return;

            UpdateLogs();
        }
    }
}