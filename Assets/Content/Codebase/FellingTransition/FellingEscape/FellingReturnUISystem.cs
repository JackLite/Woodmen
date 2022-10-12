using Core;
using EcsCore;
using Woodman.Common;
using Woodman.Felling;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.FellingEscape
{
    [EcsSystem(typeof(MainModule))]
    public class FellingReturnUISystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private MetaTreesRepository _treesRepository;
        private FellingCharacterController _characterController;
        private FellingUIProvider _fellingUIProvider;
        private WindowsUiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick += OnLoseClick;
            _uiProvider.FellingLoseWindow.OnRestartClick += OnRestartClick;
        }

        private void OnWinClick()
        {
            _uiProvider.FellingWinWindow.Hide();
        }

        private void OnLoseClick()
        {
            _characterController.SetSide(FellingSide.Right);
            _uiProvider.FellingLoseWindow.Hide();
        }

        private void OnRestartClick()
        {
            _uiProvider.FellingLoseWindow.Hide();
            _uiProvider.FellingUi.Show();
            _fellingUIProvider.StartGameBtn.gameObject.SetActive(true);
            _fellingUIProvider.FellingTimerView.SetProgress(1);
            _fellingUIProvider.TreeUIProgress.SetProgress(1);
        }

        public void Destroy()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick -= OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick -= OnLoseClick;
        }
    }
}