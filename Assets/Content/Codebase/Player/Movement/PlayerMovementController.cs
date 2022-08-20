using System;
using Woodman.Common;
using Woodman.Player.Movement.View;
using Zenject;

namespace Woodman.Player.Movement
{
    public class PlayerMovementController : IInitializable, IFixedTickable, IDisposable
    {
        private readonly ControlMovementPlayer _controlMovementPlayer;
        private readonly PlayerMovement _playerMovement;

        public PlayerMovementController(MainViewProvider viewProvider)
        {
            _playerMovement = viewProvider.PlayerMovement;
            _controlMovementPlayer = viewProvider.ControlMovementPlayer;
        }

        public bool IsPlayerMoving => _playerMovement.IsMoving;

        public void Dispose()
        {
            _controlMovementPlayer.OnStopMove -= _playerMovement.StopMove;
        }

        public void FixedTick()
        {
            _playerMovement.Move(_controlMovementPlayer.ReadInput());
        }

        public void Initialize()
        {
            _controlMovementPlayer.OnStopMove += _playerMovement.StopMove;
        }
    }
}