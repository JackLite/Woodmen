using System;
using System.Threading.Tasks;
using Woodman.Common;
using Woodman.Felling;

namespace Woodman.FellingTransition
{
    public class FellingUiSwitcher
    {
        private readonly WindowsUiProvider _windowsUiProvider;
        private readonly FellingUIProvider _uiProvider;

        public FellingUiSwitcher(WindowsUiProvider windowsUiProvider, FellingUIProvider uiProvider)
        {
            _windowsUiProvider = windowsUiProvider;
            _uiProvider = uiProvider;
        }
        
        public async void InitFelling()
        {
            _windowsUiProvider.MetaUi.Hide();
            await Task.Delay(TimeSpan.FromSeconds(2));
            _windowsUiProvider.FellingUi.Show();
            _uiProvider.StartGameBtn.gameObject.SetActive(true);
        }
    }
}