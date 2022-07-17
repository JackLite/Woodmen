using CameraProcessing;
using MetaInteractions;
using Movement;

namespace MetaTrees
{
    public class TreeInteraction
    {
        private readonly PlayerMovementController _playerMovement;
        private readonly CameraController _cameraController;
        public TreeInteraction(PlayerMovementController playerMovement, CameraController cameraController)
        {
            _playerMovement = playerMovement;
            _cameraController = cameraController;
        }

        public void OnInteract(InteractTarget target)
        {
            var tree = target.GetComponent<Tree>();
            if (!tree)
                return;

            _playerMovement.SetPlayerToPos(tree.PlayerWorldPosition);
            _cameraController.MoveToCore();
        }
    }
}