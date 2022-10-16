using Core;
using EcsCore;
using UnityEngine;
using Woodman.Felling.Timer;

namespace Woodman.Felling.Tree.Branches
{
    [EcsSystem(typeof(FellingModule))]
    public class FellingTimeFreezeSystem : IRunSystem
    {
        private EcsOneData<TimerData> _timerData;
        private FellingUIProvider _uiProvider;
        private DataWorld _world;
        
        public void Run()
        {
            var q = _world.Select<FellingTimeFreeze>().GetEntities();
            var isFreeze = false;
            foreach (var e in q)
            {
                ref var timeFreeze = ref e.GetComponent<FellingTimeFreeze>();
                if (Time.time >= timeFreeze.unfreezeTime)
                    e.AddComponent(new EcsOneFrame());
                else
                    isFreeze = true;
            }
            ref var timerData = ref _timerData.GetData();
            timerData.isFreeze = isFreeze;

            if (timerData.isFreeze)
            {
                _uiProvider.FellingTimerView.SetFreeze();
            }
        }
    }
}