using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Locations.Interactions;

namespace Woodman.Locations.Trees
{
    [EcsSystem(typeof(MetaModule))]
    public class TreesDisableSystem : IRunSystem
    {
        private DataWorld _world;
        
        public void Run()
        {
            var query = _world.Select<DisableTreesSignal>();
            if (!query.Any()) 
                return;
            
            query.DestroyAll();
            foreach(var target in InteractionStaticPool.AllTargets())
            {
                if (target.InteractType == InteractTypeEnum.Tree)
                    target.Disable();
            }
        }
    }
}