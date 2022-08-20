using System;
using System.Collections.Generic;

namespace Woodman.MetaInteractions
{
    public static class InteractionStaticPool
    {
        private static readonly Queue<InteractTarget> _targets = new();

        public static event Action OnRegister;

        public static void Register(InteractTarget interactTarget)
        {
            _targets.Enqueue(interactTarget);
            OnRegister?.Invoke();
        }

        public static IEnumerable<InteractTarget> GetTargets()
        {
            while (_targets.Count > 0) yield return _targets.Dequeue();
        }
    }
}