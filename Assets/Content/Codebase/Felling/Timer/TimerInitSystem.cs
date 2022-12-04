using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Felling.Settings;

namespace Woodman.Felling.Timer
{
    [EcsSystem(typeof(CoreModule))]
    public class TimerInitSystem : IInitSystem
    {
        private EcsOneData<FellingSettings> _fellingSettings;
        private EcsOneData<TimerData> _timerData;
        private FellingUIProvider _uiProvider;

        public void Init()
        {
            var settings = _fellingSettings.GetData();
            _timerData.SetData(new TimerData
            {
                remain = settings.time,
                totalTime = settings.time
            });
            _uiProvider.FellingTimerView.ResetFreeze();
        }
    }
}