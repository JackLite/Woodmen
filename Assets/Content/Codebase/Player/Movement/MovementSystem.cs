using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Meta;
using Woodman.Player.Movement.View;

namespace Woodman.Player.Movement
{
    [EcsSystem(typeof(MetaModule))]
    public class MovementSystem : IRunPhysicSystem
    {
        private MetaViewProvider _metaViewProvider;
        private EcsOneData<PlayerMovementData> _moveData;

        public void RunPhysic()
        {
            _metaViewProvider.PlayerMovement.Move(_moveData.GetData().input);
        }
    }
}