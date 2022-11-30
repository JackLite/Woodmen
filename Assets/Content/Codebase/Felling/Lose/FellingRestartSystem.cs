using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Felling.Pause;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;
using Woodman.Progress;

namespace Woodman.Felling.Lose
{
    [EcsSystem(typeof(CoreModule))]
    public class FellingRestartSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<SecondChanceData> _secondChanceData;
        private FellingPositions _positions;
        private FellingViewProvider _viewProvider;
        private FellingCharacterController _characterController;
        private TreeGenerator _treeGenerator;
        private TreePiecesRepository _piecesRepository;
        private UiProvider _uiProvider;
        private PauseView _pauseView;
        private ProgressionService _progressionService;

        public void Init()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick += RestartFelling;
            _pauseView.OnRestart += RestartFelling;
        }

        private void RestartFelling()
        {
            _piecesRepository.Destroy();
            ref var treeModel = ref _currentTree.GetData();
            treeModel.size = _progressionService.GetSize();
            _characterController.ResetDead();
            _characterController.SetSide(FellingSide.Right);
            _treeGenerator.Generate(_positions.RootPos, treeModel.size, _viewProvider.Environment);

            ref var scd = ref _secondChanceData.GetData();
            scd.isActive = false;
            scd.wasShowed = false;
            scd.remainTime = scd.totalTime;
            
            _world.NewEntity().AddComponent(new TimerRestartEvent());
        }

        public void Destroy()
        {
            _uiProvider.FellingLoseWindow.OnRestartClick -= RestartFelling;
            _pauseView.OnRestart -= RestartFelling;
        }
    }
}