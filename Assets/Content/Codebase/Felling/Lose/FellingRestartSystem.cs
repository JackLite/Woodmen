using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;
using Woodman.MetaTrees;

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(MainModule))]
    public class FellingRestartSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private FellingCharacterController _characterController;
        private MetaTreesRepository _metaTrees;
        private TreeGenerator _treeGenerator;
        private TreePiecesRepository _piecesRepository;
        private WindowsUiProvider _uiProvider;
        public void Init()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick += RestartFelling;
        }

        private void RestartFelling()
        {
            _piecesRepository.Destroy();
            var treeModel = _metaTrees.CurrentTree.GetTreeModel();
            _characterController.SetSide(FellingSide.Right);
            _treeGenerator.Generate(_metaTrees.CurrentTree.transform.position, treeModel.size);
            _world.NewEntity().AddComponent(new TimerRestartEvent());
        }

        public void Destroy()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick -= RestartFelling;
        }
    }
}