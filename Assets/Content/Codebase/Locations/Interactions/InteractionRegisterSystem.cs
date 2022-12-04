using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Locations.Interactions.Components;
using Woodman.Player;

namespace Woodman.Locations.Interactions
{
    [EcsSystem(typeof(MetaModule))]
    public class InteractionRegisterSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private readonly DataWorld _world;
        private EcsOneData<PlayerData> _playerData;

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
            foreach (var target in InteractionStaticPool.DequeueTargets())
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
            if (AlreadyInteract(target)) return;
            var interactStart = new InteractStart
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }

        private void OnInteract(InteractTarget target)
        {
            if (AlreadyInteract(target)) return;
            SetInteract(target);
            var interactStart = new Interact
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }

        private void OnStopInteract(InteractTarget target)
        {
            ResetInteract();
            var interactStart = new InteractStop
            {
                target = target,
                interactType = target.InteractType
            };
            _world.NewEntity().AddComponent(interactStart);
        }

        private bool AlreadyInteract(InteractTarget target)
        {
            ref var pd = ref _playerData.GetData();
            if (!pd.interact) return false;
            var distance = pd.lastInteractPosition - target.transform.position;
            return distance.sqrMagnitude <= 0.01f;
        }

        private void SetInteract(InteractTarget target)
        {
            ref var pd = ref _playerData.GetData();
            pd.interact = true;
            pd.lastInteractPosition = target.transform.position;
        }

        private void ResetInteract()
        {
            ref var pd = ref _playerData.GetData();
            pd.interact = false;
        }
    }
}