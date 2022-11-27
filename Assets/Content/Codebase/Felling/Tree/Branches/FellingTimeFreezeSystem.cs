using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
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
            var q = _world.Select<FellingTimeFreeze>();
            if (!q.Any())
                return;
            var entities = q.GetEntities();
            var isFreeze = false;
            var unfreezeMaxTime = 0f;
            foreach (var e in entities)
            {
                ref var timeFreeze = ref e.GetComponent<FellingTimeFreeze>();
                if (Time.time >= timeFreeze.unfreezeTime)
                {
                    e.AddComponent(new EcsOneFrame());
                }
                else
                {
                    if (unfreezeMaxTime < timeFreeze.unfreezeTime)
                        unfreezeMaxTime = timeFreeze.unfreezeTime;
                    isFreeze = true;
                }
            }

            ref var timerData = ref _timerData.GetData();
            timerData.isPaused = isFreeze;
            
            UpdateUI(isFreeze, ref timerData, unfreezeMaxTime);
        }

        private void UpdateUI(bool isFreeze, ref TimerData timerData, float unfreezeMaxTime)
        {
            if (isFreeze && timerData.freezeState == TimerFreezeState.Unfreeze)
            {
                timerData.freezeState = TimerFreezeState.Freeze;
                _uiProvider.FellingTimerView.SetFreeze();
                return;
            }

            if (isFreeze && timerData.freezeState == TimerFreezeState.Freeze)
            {
                var remainTime = unfreezeMaxTime - Time.time;
                if (remainTime < 3)
                {
                    timerData.freezeState = TimerFreezeState.Defroze;
                    _uiProvider.FellingTimerView.SetDefroze();
                }

                return;
            }

            if (!isFreeze && timerData.freezeState == TimerFreezeState.Defroze)
            {
                timerData.freezeState = TimerFreezeState.Unfreeze;
                _uiProvider.FellingTimerView.SetUnfreeze();
            }
        }
    }
}