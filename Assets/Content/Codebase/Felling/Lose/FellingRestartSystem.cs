using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingRestartSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private FellingPositions _positions;
        private FellingCharacterController _characterController;
        private TreeGenerator _treeGenerator;
        private TreePiecesRepository _piecesRepository;
        private UiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick += RestartFelling;
        }

        private void RestartFelling()
        {
            _piecesRepository.Destroy();
            var treeModel = _currentTree.GetData();
            _characterController.SetSide(FellingSide.Right);
            _treeGenerator.Generate(_positions.RootPos, treeModel.size);
            _world.NewEntity().AddComponent(new TimerRestartEvent());
        }

        public void Destroy()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick -= RestartFelling;
        }
    }
}