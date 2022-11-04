using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Player.Movement.View;

namespace Woodman.Player.Movement
{
    [EcsSystem(typeof(MetaModule))]
    public class MovementSystem : IRunPhysicSystem
    {
        private MainViewProvider _mainViewProvider;
        private EcsOneData<PlayerMovementData> _moveData;

        public void RunPhysic()
        {
            _mainViewProvider.PlayerMovement.Move(_moveData.GetData().input);
        }
    }
}