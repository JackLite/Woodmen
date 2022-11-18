using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Felling.Start;
using Woodman.Locations.Interactions.Components;
using Woodman.Locations.Trees;
using Woodman.Player.Indicators;

namespace Woodman.Locations.Interactions.TreeInteraction
{
    [EcsSystem(typeof(MetaModule))]
    public class TreeInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerIndicatorsController _indicatorsController;
        public void Run()
        {
            var startQuery = _world.Select<InteractStart>()
                .Where<InteractStart>(c => c.interactType == InteractTypeEnum.Tree);
            if (startQuery.Any())
                _indicatorsController.ToggleTreeIndicator(true);
            
            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Tree);
            if (q.TrySelectFirst(out Interact interact))
            {
                var tree = (interact.target as TreeInteract)?.TreeMeta;
                if (tree)
                    _world.CreateOneFrame().AddComponent(new MoveToFelling { treeMeta = tree });
            }
            
            var stopQuery = _world.Select<InteractStop>()
                .Where<InteractStop>(c => c.interactType == InteractTypeEnum.Tree);
            if (stopQuery.Any())
                _indicatorsController.ToggleTreeIndicator(false);
        }
    }
}