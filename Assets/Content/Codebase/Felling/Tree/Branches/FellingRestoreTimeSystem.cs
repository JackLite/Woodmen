using Core;
using EcsCore;
using Woodman.Felling.Timer;

namespace Woodman.Felling.Tree.Branches
{
    [EcsSystem(typeof(FellingModule))]
    public class FellingRestoreTimeSystem : IRunSystem
    {
        private EcsOneData<TimerData> _timerData;
        private DataWorld _world;
        
        public void Run()
        {
            var entities = _world.Select<FellingRestoreTimeEvent>().GetEntities();
            ref var td = ref _timerData.GetData();
            foreach (var e in entities)
            {
                td.remain = td.totalTime;
                e.AddComponent(new EcsOneFrame());
            }
        }
    }
}