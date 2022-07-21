using Woodman.CameraProcessing;
using Woodman.Felling.Tree;
using Woodman.MetaTrees;

namespace Woodman.Felling
{
    public class FellingInitializer
    {
        private readonly FellingUI _fellingUI;
        private readonly FellingCharacterController _characterController;
        private readonly CameraController _cameraController;
        private readonly TreeGenerator _treeGenerator;

        public FellingInitializer(
            FellingUI fellingUI,
            FellingCharacterController characterController,
            CameraController cameraController,
            TreeGenerator treeGenerator)
        {
            _fellingUI = fellingUI;
            _characterController = characterController;
            _cameraController = cameraController;
            _treeGenerator = treeGenerator;
        }

        public void Init(TreeMeta tree)
        {
            _characterController.InitFelling(tree);
            _characterController.SetSide(Side.Right);
            _fellingUI.InitFelling();
            _cameraController.MoveToCore(tree.transform);
            _treeGenerator.Generate(tree.transform.position);
            tree.HideMesh();
            tree.gameObject.SetActive(false);
        }
    }
}