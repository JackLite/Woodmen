using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common.CameraProcessing;
using Woodman.FellingTransition.TransitionToFelling;
using Woodman.MetaInteractions.Components;
using Woodman.MetaTrees;
using Woodman.Player.Indicators;

namespace Woodman.MetaInteractions.TreeInteraction
{
    [EcsSystem(typeof(MetaModule))]
    public class TreeInteractionSystem : IRunSystem
    {
        private DataWorld _world;
        private PlayerIndicatorsController _indicatorsController;
        private CameraController _cameraController;
        public void Run()
        {
            var startQuery = _world.Select<InteractStart>()
                .Where<InteractStart>(c => c.interactType == InteractTypeEnum.Tree);
            if (startQuery.Any())
                _indicatorsController.ShowHideTreeIndicator(true);
            
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
                _indicatorsController.ShowHideTreeIndicator(false);
        }
    }
}