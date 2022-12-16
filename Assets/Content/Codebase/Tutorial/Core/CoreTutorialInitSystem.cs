using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Timer;

namespace Woodman.Tutorial.Core
{
    [EcsSystem(typeof(CoreModule))]
    public class CoreTutorialInitSystem : IInitSystem, IActivateSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<CoreTutorialData> _tutorialData;
        private EcsOneData<TimerData> _timer;
        public void Init()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete)
                return;
            td.branchComplete = false;
            td.leftTapComplete = false;
            td.rightTapComplete = false;
            td.timerComplete = false;
            td.tapsCount = 0;
            _world.InitModule<CoreTutorialModule, CoreModule>(true);
        }

        public void Activate()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete)
                return;
            ref var timer = ref _timer.GetData();
            timer.isPaused = true;
        }

        public void Destroy()
        {
            _world.DestroyModule<CoreTutorialModule>();
        }
    }
}