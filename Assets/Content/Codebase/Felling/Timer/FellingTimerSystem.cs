using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling.Settings;

namespace Woodman.Felling.Timer
{
    [EcsSystem(typeof(FellingModule))]
    public class FellingTimerSystem : IActivateSystem, IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TimerData> _timerData;
        private EcsOneData<FellingSettings> _fellingSettings;
        private FellingUIProvider _uiProvider;

        public void Run()
        {
            ref var td = ref _timerData.GetData();
            if (td.isFreeze)
                return;
            var restartQ = _world.Select<TimerRestartEvent>();
            if (restartQ.Any())
            {
                restartQ.DestroyAll();
                td.remain = td.totalTime;
            }

            if (td.remain <= 0 && !_world.Select<TimerEndEvent>().Any())
            {
                _world.NewEntity().AddComponent(new TimerEndEvent());
                return;
            }

            td.remain -= Time.deltaTime;
            _uiProvider.FellingTimerView.SetProgress(td.remain / td.totalTime);
        }

        public void Activate()
        {
            var settings = _fellingSettings.GetData();
            _timerData.SetData(new TimerData
            {
                remain = settings.time,
                totalTime = settings.time
            });
        }
    }
}