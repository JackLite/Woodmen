using Core;
using EcsCore;
using Woodman.Common;

namespace Woodman.Player.Movement
{
    [EcsSystem(typeof(MainModule))]
    public class MovementSystem : IInitSystem, IRunPhysicSystem, IDestroySystem
    {
        private MainViewProvider _mainViewProvider;

        public void Init()
        {
            _mainViewProvider.ControlMovementPlayer.OnStopMove += _mainViewProvider.PlayerMovement.StopMove;
        }

        public void RunPhysic()
        {
            _mainViewProvider.PlayerMovement.Move(_mainViewProvider.ControlMovementPlayer.ReadInput());
        }

        public void Destroy()
        {
            _mainViewProvider.ControlMovementPlayer.OnStopMove -= _mainViewProvider.PlayerMovement.StopMove;
        }
    }
}