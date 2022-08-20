using System;
using System.Threading.Tasks;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.Logs;
using Woodman.MetaTrees;
using Zenject;

namespace Woodman.Common
{
    public class MetaCoreTransition : IInitializable, IDisposable
    {
        private readonly CameraController _cameraController;
        private readonly FellingEventBus _fellingEventBus;
        private readonly LogsService _logsService;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly FellingCharacterController _characterController;
        private readonly MetaTreesRepository _treesRepository;
        private readonly WindowsUiProvider _uiProvider;

        public MetaCoreTransition(FellingEventBus fellingEventBus, WindowsUiProvider uiProvider,
            CameraController cameraController, MetaTreesRepository treesRepository, LogsService logsService,
            TreePiecesRepository treePiecesRepository, FellingCharacterController characterController)
        {
            _fellingEventBus = fellingEventBus;
            _uiProvider = uiProvider;
            _cameraController = cameraController;
            _treesRepository = treesRepository;
            _logsService = logsService;
            _treePiecesRepository = treePiecesRepository;
            _characterController = characterController;
        }

        public void Initialize()
        {
            _fellingEventBus.OnEscapeFelling += OnEscapeFelling;
        }

        public void Dispose()
        {
            _fellingEventBus.OnEscapeFelling -= OnEscapeFelling;
        }

        private async void OnEscapeFelling(bool isWin)
        {
            if (isWin)
            {
                _logsService.ShowLogsAfterFelling(_treesRepository.CurrentTree);
                _uiProvider.FellingWinWindow.Hide();
            }
            else
            {
                _characterController.SetSide(FellingSide.Right);
                _uiProvider.FellingLoseWindow.Hide();
            }

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
    }
}