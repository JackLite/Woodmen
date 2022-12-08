using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling.Finish;
using Woodman.Felling.Finish.Lose;
using Woodman.Felling.Tree;
using Woodman.Progress;
using Woodman.Utils;

namespace Woodman.Felling.SecondChance
{
    [EcsSystem(typeof(CoreModule))]
    public class SecondChanceUpdateSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<SecondChanceData> _secondChanceData;
        private EcsOneData<TreeModel> _tree;
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
                _secondChanceView.SetTime(d.remainTime, d.totalTime);
                _secondChanceView.Hide();
                _world.CreateEvent(new FellingFinishSignal
                {
                    reason = FellingFinishReason.Lose,
                    loseReason = d.loseReason,
                    progress = _tree.GetData().progress,
                    secondChanceShowed = true
                });
                _progressionService.RegisterCoreResult(false);
                _fellingLoseWindow.Show();
            }
        }
    }
}