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
        private readonly TreeProgressService _progressService;

        public FellingInitializer(
            FellingUI fellingUI,
            FellingCharacterController characterController,
            CameraController cameraController,
            TreeGenerator treeGenerator,
            TreeProgressService progressService)
        {
            _fellingUI = fellingUI;
            _characterController = characterController;
            _cameraController = cameraController;
            _treeGenerator = treeGenerator;
            _progressService = progressService;
        }

        public void Init(TreeMeta tree)
        {
            var treeModel = tree.GetTreeModel();
            _progressService.SetModel(treeModel);
            _characterController.InitFelling(treeModel);
            _characterController.SetSide(Side.Right);
            _fellingUI.InitFelling();
            _cameraController.MoveToCore(tree.transform);
            _treeGenerator.Generate(tree.transform.position, treeModel.size);
            tree.HideMesh();
            tree.gameObject.SetActive(false);
        }
    }
}