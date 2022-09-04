using System;
using System.Threading.Tasks;
using Core;
using EcsCore;
using Woodman.Common;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.Logs;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.FellingEscape
{
    [EcsSystem(typeof(MainModule))]
    public class FellingReturnViewSystem: IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private CameraController _cameraController;
        private MetaTreesRepository _treesRepository;
        private FellingCharacterController _characterController;
        private LogsService _logsService;
        private TreePiecesRepository _treePiecesRepository;
        private WindowsUiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick += OnLoseClick;
        }

        private void OnWinClick()
        {
            ProcessReturn(true).Forget();
        }
        
        private void OnLoseClick()
        {
            ProcessReturn(false).Forget();
        }

        private async Task ProcessReturn(bool isWin)
        {
            _uiProvider.FellingUi.Hide();
            _cameraController.MoveToMeta();
            await Task.Delay(TimeSpan.FromSeconds(2));
            if (!isWin)
            {
                _treePiecesRepository.Destroy();
                _treesRepository.CurrentTree.EnableMeta();
            }

            _uiProvider.MetaUi.Show();
        }

        public void Destroy()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick -= OnWinClick;
            _uiProvider.FellingLoseWindow.OnHomeClick -= OnLoseClick;
        }
    }
}