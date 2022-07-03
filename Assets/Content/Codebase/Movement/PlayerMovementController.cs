using System;
using Zenject;

namespace Movement
{
    public class PlayerMovementController : IInitializable, IDisposable
    {
        private readonly PlayerMovement _playerMovement;
        private readonly ControlMovementPlayer _controlMovementPlayer;

        public PlayerMovementController(MetaViewProvider viewProvider)
        {
            _playerMovement = viewProvider.PlayerMovement;
            _controlMovementPlayer = viewProvider.ControlMovementPlayer;
        }

        public void Initialize()
        {
            _controlMovementPlayer.OnChange += _playerMovement.Move;
        }

        public void Dispose()
        {
            _controlMovementPlayer.OnChange -= _playerMovement.Move;
        }
    }
}