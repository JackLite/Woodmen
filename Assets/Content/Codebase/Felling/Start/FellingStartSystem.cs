using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.SecondChance;
using Woodman.Utils;

namespace Woodman.Felling.Start
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingStartSystem : IInitSystem, IDestroySystem
    {
        private EcsOneData<SecondChanceData> _secondChanceData;
        private DataWorld _world;
        private FellingUIProvider _fellingUIProvider;
        private FellingUi _fellingUi;

        public void Init()
        {
            _fellingUi.Show();
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(true);
            _fellingUIProvider.FellingTimerView.SetProgress(1);
            _fellingUIProvider.TreeUIProgress.SetProgress(1);
            _fellingUIProvider.TapController.OnTap += OnTap;
            
            ref var scd = ref _secondChanceData.GetData();
            scd.isActive = false;
            scd.wasShowed = false;
            scd.remainTime = scd.totalTime;
        }

        private void OnTap(FellingSide side)
        {
            if (_world.IsModuleActive<FellingModule>())
                return;
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(false);
            _world.CreateEvent<StartFellingSignal>();
            _world.ActivateModule<FellingModule>();
        }

        public void Destroy()
        {
            _fellingUIProvider.TapController.OnTap -= OnTap;
        }
    }
}