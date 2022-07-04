using System;
using Zenject;

namespace Movement
{
    public class PlayerMovementController : IInitializable, IFixedTickable, IDisposable
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
            _controlMovementPlayer.OnStopMove += _playerMovement.StopMove;
        }
        
        public void FixedTick()
        {
            _playerMovement.Move(_controlMovementPlayer.ReadInput());
        }

        public void Dispose()
        {
            _controlMovementPlayer.OnStopMove -= _playerMovement.StopMove;
        }
    }
}