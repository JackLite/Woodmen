using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;

namespace Woodman.Felling.Pause
{
    /// <summary>
    /// Implements pause in core gameplay
    /// </summary>
    [EcsSystem(typeof(CoreModule))]
    public class PauseSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private PauseView _pauseView;
        private FellingUi _fellingUi;

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