using System;
using System.Threading.Tasks;
using Woodman.Felling.Timer;
using Zenject;

namespace Woodman.Felling
{
    public class FellingUI : IInitializable
    {
        private readonly FellingUIProvider _uiProvider;
        private readonly WindowsSwitcher _windowsSwitcher;
        private readonly FellingTimer _fellingTimer;

        /// <summary>
        /// Вызывается когда игрок начинает кор-геймплей
        /// </summary>
        public event Action OnStart;

        public FellingUI(FellingUIProvider uiProvider, WindowsSwitcher windowsSwitcher, FellingTimer fellingTimer)
        {
            _uiProvider = uiProvider;
            _windowsSwitcher = windowsSwitcher;
            _fellingTimer = fellingTimer;
        }

        public void Initialize()
        {
            _uiProvider.StartGameBtn.onClick.AddListener(OnStartClick);
        }

        public async void InitFelling()
        {
            _windowsSwitcher.ShowHideMeta(false);
            await Task.Delay(TimeSpan.FromSeconds(2));
            _windowsSwitcher.ShowHideCore(true);
            _fellingTimer.Start(10f);
        }

        private void OnStartClick()
        {
            _uiProvider.StartGameBtn.gameObject.SetActive(false);
            OnStart?.Invoke();
        }
    }
}