using Core;
using EcsCore;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.TransitionToFelling
{
    [EcsSystem(typeof(MainModule))]
    public class TransitionToFellingSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private CameraController _cameraController;
        private FellingCharacterController _characterController;
        private FellingUiSwitcher _fellingUiSwitcher;
        private TreeGenerator _treeGenerator;
        private MetaTreesRepository _metaTrees;
        public void Run()
        {
            var q = _world.Select<MoveToFelling>();
            if (!q.TrySelectFirst(out MoveToFelling c)) 
                return;

            _metaTrees.CurrentTree = c.treeMeta;
            var treeModel = c.treeMeta.GetTreeModel();
            _currentTree.SetData(treeModel);
            _characterController.InitFelling(treeModel);
            _characterController.SetSide(FellingSide.Right);
            _fellingUiSwitcher.InitFelling();
            _cameraController.MoveToCore(c.treeMeta.transform);
            _treeGenerator.Generate(c.treeMeta.transform.position, treeModel.size);
            c.treeMeta.DisableMeta();
        }
    }
}