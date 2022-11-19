using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common.Tweens;
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
            CheckStartInteraction();

            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Tree);
            if (q.TrySelectFirst(out Interact interact))
            {
                var tree = (interact.target as TreeInteract)?.TreeMeta;
                if (tree)
                    _world.CreateOneFrame().AddComponent(new MoveToFelling { treeMeta = tree });
            }

            CheckEndInteraction();
        }

        private void CheckEndInteraction()
        {
            var stopQuery = _world.Select<InteractStop>()
                .Where<InteractStop>(c => c.interactType == InteractTypeEnum.Tree);
            if (stopQuery.Any())
                _indicatorsController.ToggleTreeIndicator(false);
        }

        private void CheckStartInteraction()
        {
            var startQuery = _world.Select<InteractStart>()
                .Where<InteractStart>(c => c.interactType == InteractTypeEnum.Tree);
            if (!startQuery.TrySelectFirst(out InteractStart interact))
                return;

            if (_indicatorsController == null) return;
            _indicatorsController.ToggleTreeIndicator(true);
            var tween = new TweenData
            {
                remain = interact.target.Delay,
                update = r => _indicatorsController.SetTreeProgress((interact.target.Delay - r) / interact.target.Delay),
                validate = () => _indicatorsController != null && _indicatorsController.IsTreeInteractActive()
            };
            _world.NewEntity().AddComponent(tween);
        }
    }
}