using Woodman.Common.CameraProcessing;
using Woodman.Felling.Tree;
using Woodman.MetaTrees;

namespace Woodman.Felling
{
    public class FellingInitializer
    {
        private readonly FellingUiProcessor _fellingUiProcessor;
        private readonly FellingCharacterController _characterController;
        private readonly CameraController _cameraController;
        private readonly TreeGenerator _treeGenerator;
        private readonly TreeProgressService _progressService;

        public FellingInitializer(
            FellingUiProcessor fellingUiProcessor,
            FellingCharacterController characterController,
            CameraController cameraController,
            TreeGenerator treeGenerator,
            TreeProgressService progressService)
        {
            _fellingUiProcessor = fellingUiProcessor;
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
            _characterController.SetSide(FellingSide.Right);
            _fellingUiProcessor.InitFelling();
            _cameraController.MoveToCore(tree.transform);
            _treeGenerator.Generate(tree.transform.position, treeModel.size);
            tree.HideMesh();
            tree.gameObject.SetActive(false);
        }
    }
}