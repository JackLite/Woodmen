using System;
using System.Collections.Generic;

namespace Woodman.Locations.Interactions
{
    public static class InteractionStaticPool
    {
        private static readonly Queue<InteractTarget> _targets = new();
        private static readonly List<InteractTarget> _allTargets = new();
        public static event Action OnRegister;

        public static void Register(InteractTarget interactTarget)
        {
            _allTargets.Add(interactTarget);
            _targets.Enqueue(interactTarget);
            OnRegister?.Invoke();
        }

        public static IEnumerable<InteractTarget> DequeueTargets()
        {
            while (_targets.Count > 0)
                yield return _targets.Dequeue();
        }

        public static IEnumerable<InteractTarget> AllTargets()
        {
            return _allTargets;
        }

        public static void Unregister(InteractTarget interactTarget)
        {
            _allTargets.Remove(interactTarget);
        }
    }
}