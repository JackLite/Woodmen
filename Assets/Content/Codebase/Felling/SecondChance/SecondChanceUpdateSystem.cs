using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling.Lose;
using Woodman.Progress;

namespace Woodman.Felling.SecondChance
{
    [EcsSystem(typeof(CoreModule))]
    public class SecondChanceUpdateSystem : IRunSystem
    {
        private EcsOneData<SecondChanceData> _secondChanceData;
        private FellingLoseWindow _fellingLoseWindow;
        private ProgressionService _progressionService;
        private SecondChanceView _secondChanceView;

        public void Run()
        {
            ref var d = ref _secondChanceData.GetData();
            if (!d.isActive || (d.wasShowed && d.remainTime <= 0)) 
                return;
            d.remainTime -= Time.deltaTime;
            _secondChanceView.SetTime(d.remainTime, d.totalTime);
            
            if (d.remainTime <= 0)
            {
                d.remainTime = 0;
                _secondChanceView.Hide();
                _progressionService.RegisterCoreResult(false);
                _fellingLoseWindow.Show();
            }
        }
    }
}