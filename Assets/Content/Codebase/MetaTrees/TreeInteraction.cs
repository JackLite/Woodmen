using System;
using System.Threading.Tasks;
using CameraProcessing;
using DefaultNamespace;
using MetaInteractions;
using Movement;
using Player.Indicators;

namespace MetaTrees
{
    public class TreeInteraction
    {
        private readonly PlayerMovementController _playerMovement;
        private readonly CameraController _cameraController;
        private readonly WindowsSwitcher _windowsSwitcher;
        private readonly PlayerIndicatorsController _playerIndicators;
        public TreeInteraction(
            PlayerMovementController playerMovement,
            CameraController cameraController,
            WindowsSwitcher windowsSwitcher,
            PlayerIndicatorsController playerIndicators)
        {
            _playerMovement = playerMovement;
            _cameraController = cameraController;
            _windowsSwitcher = windowsSwitcher;
            _playerIndicators = playerIndicators;
        }

        public void OnStartInteract(InteractTarget target)
        {
            _playerIndicators.ShowHideTreeIndicator(true);
        }

        public void OnEndInteract(InteractTarget target)
        {
            _playerIndicators.ShowHideTreeIndicator(false);
        }

        public void OnInteract(InteractTarget target)
        {
            var tree = target.GetComponent<Tree>();
            if (!tree)
                return;

            _playerMovement.SetPlayerToPos(tree.PlayerWorldPosition);
            _cameraController.MoveToCore();
            SwitchUi();
        }

        private async void SwitchUi()
        {
            _windowsSwitcher.ShowHideMeta(false);
            await Task.Delay(TimeSpan.FromSeconds(2));
            _windowsSwitcher.ShowHideCore(true);
        }
    }
}