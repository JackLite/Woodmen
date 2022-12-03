using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Tree;

namespace Woodman.Felling.Pause
{
    /// <summary>
    /// Implements pause in core gameplay
    /// </summary>
    [EcsSystem(typeof(CoreModule))]
    public class PauseSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _tree;
        private FellingUi _fellingUi;
        private PauseView _pauseView;
        private TreePiecesRepository _piecesRepository;

        public void Init()
        {
            _fellingUi.OnPause += OnPause;
            _pauseView.OnPlay += Play;
            _pauseView.OnRestart += Restart;
        }

        private void Restart()
        {
            _pauseView.Hide();
        }

        private void Play()
        {
            _pauseView.Hide();
            var playerStartChop = _tree.GetData().size > _piecesRepository.GetRemain();
            if (playerStartChop)
                _world.ActivateModule<FellingModule>();
        }

        private void OnPause()
        {
            _pauseView.Show();
            _world.DeactivateModule<FellingModule>();
        }

        public void Destroy()
        {
            _fellingUi.OnPause -= OnPause;
            _pauseView.OnPlay -= Play;
            _pauseView.OnRestart -= Restart;
        }
    }
}