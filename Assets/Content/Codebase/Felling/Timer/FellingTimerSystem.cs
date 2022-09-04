using Core;
using EcsCore;
using UnityEngine;

namespace Woodman.Felling.Timer
{
    [EcsSystem(typeof(FellingModule))]
    public class FellingTimerSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TimerData> _timerData;
        private FellingUIProvider _uiProvider;
        
        public void Run()
        {
            ref var td = ref _timerData.GetData();
            if (td.remain <= 0 && !_world.Select<TimerEndEvent>().Any())
            {
                _world.NewEntity().AddComponent(new TimerEndEvent());
                return;
            }

            td.remain -= Time.deltaTime;
            _uiProvider.FellingTimerView.SetProgress(td.remain / td.totalTime);
        }
    }
}