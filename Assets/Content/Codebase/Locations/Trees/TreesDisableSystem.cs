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
            var list = InteractionStaticPool.AllTargets();
            for (var i = 0; i < list.Count;)
            {
                var target = list[i];
                if (target.InteractType == InteractTypeEnum.Tree)
                    target.Disable();
                else
                    i++;
            }
        }
    }
}