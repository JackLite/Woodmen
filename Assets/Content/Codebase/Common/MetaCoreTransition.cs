using System;
using System.Threading.Tasks;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Zenject;

namespace Woodman.Common
{
    public class MetaCoreTransition : IInitializable, IDisposable
    {
        private readonly FellingEventBus _fellingEventBus;
        private readonly WindowsUiProvider _uiProvider;
        private readonly CameraController _cameraController;

        public MetaCoreTransition(FellingEventBus fellingEventBus, WindowsUiProvider uiProvider,
            CameraController cameraController)
        {
            _fellingEventBus = fellingEventBus;
            _uiProvider = uiProvider;
            _cameraController = cameraController;
        }

        public void Initialize()
        {
            _fellingEventBus.OnEscapeFelling += OnEscapeFelling;
        }

        private async void OnEscapeFelling(bool isWin)
        {
            _uiProvider.FellingUi.Hide();
            _uiProvider.FellingWinWindow.Hide();
            _cameraController.MoveToMeta();
            await Task.Delay(TimeSpan.FromSeconds(2));
            _uiProvider.MetaUi.Show();
        }

        public void Dispose()
        {
            _fellingEventBus.OnEscapeFelling -= OnEscapeFelling;
        }
    }
}