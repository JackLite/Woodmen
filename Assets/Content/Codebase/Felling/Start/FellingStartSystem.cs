using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;

namespace Woodman.Felling.Start
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingStartSystem : IInitSystem, IDestroySystem
    {
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
        }

        private void OnTap(FellingSide side)
        {
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(false);
            _world.ActivateModule<FellingModule>();
        }

        public void Destroy()
        {
            _fellingUIProvider.TapController.OnTap -= OnTap;
        }
    }
}