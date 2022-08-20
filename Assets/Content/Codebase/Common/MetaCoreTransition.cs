using System;
using System.Threading.Tasks;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
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
        private readonly MetaTreesRepository _treesRepository;
        private readonly WindowsUiProvider _uiProvider;

        public MetaCoreTransition(FellingEventBus fellingEventBus, WindowsUiProvider uiProvider,
            CameraController cameraController, MetaTreesRepository treesRepository, LogsService logsService)
        {
            _fellingEventBus = fellingEventBus;
            _uiProvider = uiProvider;
            _cameraController = cameraController;
            _treesRepository = treesRepository;
            _logsService = logsService;
        }

        public void Dispose()
        {
            _fellingEventBus.OnEscapeFelling -= OnEscapeFelling;
        }

        public void Initialize()
        {
            _fellingEventBus.OnEscapeFelling += OnEscapeFelling;
        }

        private async void OnEscapeFelling(bool isWin)
        {
            if (isWin)
                _logsService.ShowLogsAfterFelling(_treesRepository.CurrentTree);
            _uiProvider.FellingUi.Hide();
            _uiProvider.FellingWinWindow.Hide();
            _cameraController.MoveToMeta();
            await Task.Delay(TimeSpan.FromSeconds(2));
            _uiProvider.MetaUi.Show();
        }
    }
}