using System;
using System.Threading.Tasks;
using Woodman.Common;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingUiProcessor : IInitializable
    {
        private readonly TreeProgressService _progressService;
        private readonly FellingUIProvider _uiProvider;
        private readonly WindowsUiProvider _windowsUiProvider;

        public FellingUiProcessor(
            FellingUIProvider uiProvider,
            TreeProgressService progressService,
            WindowsUiProvider windowsUiProvider)
        {
            _uiProvider = uiProvider;
            _progressService = progressService;
            _windowsUiProvider = windowsUiProvider;
        }

        public void Initialize()
        {
            _uiProvider.StartGameBtn.onClick.AddListener(OnStartClick);
            _progressService.OnProgressChange += OnProgressChange;
        }

        /// <summary>
        ///     Вызывается когда игрок начинает кор-геймплей
        /// </summary>
        public event Action OnStart;

        public async void InitFelling()
        {
            _windowsUiProvider.MetaUi.Hide();
            await Task.Delay(TimeSpan.FromSeconds(2));
            _windowsUiProvider.FellingUi.Show();
            _uiProvider.StartGameBtn.gameObject.SetActive(true);
        }

        private void OnStartClick()
        {
            _uiProvider.StartGameBtn.gameObject.SetActive(false);
            OnStart?.Invoke();
        }

        private void OnProgressChange()
        {
            var f = (float)_progressService.GetRemain() / _progressService.GetTotalSize();
            _uiProvider.TreeUIProgress.SetProgress(f);
        }
    }
}