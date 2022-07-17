using System.Collections.Generic;

namespace MetaInteractions
{
    public static class InteractionStaticPool
    {
        private static readonly List<InteractTarget> _targets = new();
        public static IReadOnlyList<InteractTarget> Targets => _targets;

        public static void Register(InteractTarget interactTarget)
        {
            _targets.Add(interactTarget);
        }
    }
}