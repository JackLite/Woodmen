using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling;

namespace Woodman.FellingTransition.FellingStart
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingStartSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private FellingUIProvider _fellingUIProvider;

        public void Init()
        {
            _fellingUIProvider.StartGameBtn.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(false);
            _world.ActivateModule<FellingModule>();
        }

        public void Destroy()
        {
            _fellingUIProvider.StartGameBtn.onClick.RemoveListener(OnStartClick);
        }
    }
}