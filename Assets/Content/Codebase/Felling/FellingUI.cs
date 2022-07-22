using System;
using System.Threading.Tasks;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingUI : IInitializable
    {
        private readonly FellingUIProvider _uiProvider;
        private readonly WindowsSwitcher _windowsSwitcher;
        private readonly FellingTimer _fellingTimer;
        private readonly TreeProgressService _progressService;
        private readonly FellingSettingsContainer _settingsContainer;

        /// <summary>
        /// Вызывается когда игрок начинает кор-геймплей
        /// </summary>
        public event Action OnStart;

        public FellingUI(
            FellingUIProvider uiProvider,
            WindowsSwitcher windowsSwitcher,
            FellingTimer fellingTimer,
            TreeProgressService progressService,
            FellingSettingsContainer settingsContainer)
        {
            _uiProvider = uiProvider;
            _windowsSwitcher = windowsSwitcher;
            _fellingTimer = fellingTimer;
            _progressService = progressService;
            _settingsContainer = settingsContainer;
        }

        public void Initialize()
        {
            _uiProvider.StartGameBtn.onClick.AddListener(OnStartClick);
            _progressService.OnProgressChange += OnProgressChange;
        }

        public async void InitFelling()
        {
            _windowsSwitcher.ShowHideMeta(false);
            await Task.Delay(TimeSpan.FromSeconds(2));
            _windowsSwitcher.ShowHideCore(true);
            _fellingTimer.Start(_settingsContainer.GetSettings().time);
        }

        private void OnStartClick()
        {
            _uiProvider.StartGameBtn.gameObject.SetActive(false);
            OnStart?.Invoke();
        }

        private void OnProgressChange()
        {
            var f = (float) _progressService.GetRemain() / _progressService.GetTotalSize();
            _uiProvider.TreeUIProgress.SetProgress(f);
        }
    }
}