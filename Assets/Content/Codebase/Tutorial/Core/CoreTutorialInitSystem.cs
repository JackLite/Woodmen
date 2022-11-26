using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Timer;

namespace Woodman.Tutorial.Core
{
    [EcsSystem(typeof(CoreModule))]
    public class CoreTutorialInitSystem : IInitSystem, IActivateSystem
    {
        private DataWorld _world;
        private EcsOneData<CoreTutorialData> _tutorialData;
        private EcsOneData<TimerData> _timer;
        public void Init()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete)
                return;
            
            _world.InitModule<CoreTutorialModule, CoreModule>();
            _world.ActivateModule<CoreTutorialModule>();
        }

        public void Activate()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete)
                return;
            ref var timer = ref _timer.GetData();
            timer.isFreeze = true;
        }
    }
}