using Core;
using EcsCore;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Logs;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.FellingEscape
{
    [EcsSystem(typeof(MainModule))]
    public class FellingReturnUISystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private MetaTreesRepository _treesRepository;
        private FellingCharacterController _characterController;
        private LogsService _logsService;
        private WindowsUiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick += OnLoseClick;
        }

        private void OnWinClick()
        {
            _logsService.ShowLogsAfterFelling(_treesRepository.CurrentTree);
            _uiProvider.FellingWinWindow.Hide();
        }

        private void OnLoseClick()
        {
            _characterController.SetSide(FellingSide.Right);
            _uiProvider.FellingLoseWindow.Hide();
        }

        public void Destroy()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick -= OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick -= OnLoseClick;
        }
    }
}