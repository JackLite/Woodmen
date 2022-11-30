using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Lose;
using Woodman.Felling.Timer;
using Woodman.Player.PlayerResources;
using Woodman.Progress;

namespace Woodman.Felling.SecondChance
{
    [EcsSystem(typeof(CoreModule))]
    public class SecondChanceUseSystem : IInitSystem, IDestroySystem
    {
        private SecondChanceView _secondChanceView;
        private FellingUi _fellingUi;
        private FellingLoseWindow _fellingLoseWindow;
        private PlayerCoinsRepository _coinsRepository;
        private EcsOneData<TimerData> _timerData;
        private EcsOneData<SecondChanceData> _secondChanceData;
        private DataWorld _world;
        private FellingCharacterController _characterController;
        private ProgressionService _progressionService;

        public void Init()
        {
            _secondChanceView.OnUseSecondChance += UseSecondChance;
            _secondChanceView.OnSkip += Skip;
        }

        private void UseSecondChance()
        {
            _coinsRepository.SubtractRes(5);
            ref var td = ref _timerData.GetData();
            td.remain = td.totalTime;
            ref var scd = ref _secondChanceData.GetData();
            scd.isActive = false;

            if (scd.loseReason != LoseReason.TimeOut)
            {
                var oldSide = _characterController.CurrentFellingSide;
                var newSide = oldSide == FellingSide.Left ? FellingSide.Right : FellingSide.Left;
                _characterController.SetSide(newSide);
            }
            _characterController.ResetDead();

            _secondChanceView.Hide();
            _fellingUi.Show();
            _world.ActivateModule<FellingModule>();
        }

        private void Skip()
        {
            ref var scd = ref _secondChanceData.GetData();
            scd.isActive = false;
            _progressionService.RegisterCoreResult(false);
            _secondChanceView.Hide();
            _fellingLoseWindow.Show();
        }

        public void Destroy()
        {
            _secondChanceView.OnUseSecondChance -= UseSecondChance;
            _secondChanceView.OnSkip -= Skip;
        }
    }
}