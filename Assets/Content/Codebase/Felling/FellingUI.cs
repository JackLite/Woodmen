using System;
using System.Threading.Tasks;
using Zenject;

namespace Woodman.Felling
{
    public class FellingUI : IInitializable
    {
        private readonly FellingUIProvider _uiProvider;
        private readonly WindowsSwitcher _windowsSwitcher;

        /// <summary>
        /// Вызывается когда игрок начинает кор-геймплей
        /// </summary>
        public event Action OnStart;

        public FellingUI(FellingUIProvider uiProvider, WindowsSwitcher windowsSwitcher)
        {
            _uiProvider = uiProvider;
            _windowsSwitcher = windowsSwitcher;
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
        }

        private void OnStartClick()
        {
            _uiProvider.StartGameBtn.gameObject.SetActive(false);
            OnStart?.Invoke();
        }
    }
}