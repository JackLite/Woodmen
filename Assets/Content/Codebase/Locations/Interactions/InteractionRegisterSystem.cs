using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Locations.Interactions.Components;

namespace Woodman.Locations.Interactions
{
    [EcsSystem(typeof(MetaModule))]
    public class InteractionRegisterSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private readonly DataWorld _world;

        public void Init()
        {
            InteractionStaticPool.OnRegister += OnRegisterTarget;
            OnRegisterTarget();
        }

        public void Run()
        {
            foreach (var e in _world.Select<InteractStart>().GetEntities())
                e.AddComponent(new EcsOneFrame());
            foreach (var e in _world.Select<Interact>().GetEntities())
                e.AddComponent(new EcsOneFrame());
            foreach (var e in _world.Select<InteractStop>().GetEntities())
                e.AddComponent(new EcsOneFrame());
        }

        public void Destroy()
        {
            InteractionStaticPool.OnRegister -= OnRegisterTarget;
        }

        private void OnRegisterTarget()
        {
            foreach (var target in InteractionStaticPool.GetTargets())
                Subscribe(target);
        }

        private void Subscribe(InteractTarget target)
        {
            target.OnStartInteract += OnStartInteract;
            target.OnInteract += OnInteract;
            target.OnEndInteract += OnStopInteract;
        }

        private void OnStartInteract(InteractTarget target)
        {
            var interactStart = new InteractStart
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }

        private void OnInteract(InteractTarget target)
        {
            var interactStart = new Interact
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }

        private void OnStopInteract(InteractTarget target)
        {
            var interactStart = new InteractStop
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }
    }
}