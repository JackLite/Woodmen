using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;
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
            var root = _treeGenerator.Generate(c.treeMeta.transform.position, treeModel.size);
            _characterController.InitFelling(treeModel, root);
            _characterController.SetSide(FellingSide.Right);
            _fellingUiSwitcher.InitFelling();
            _cameraController.MoveToCore(root.transform);
            c.treeMeta.DisableMeta();
        }
    }
}