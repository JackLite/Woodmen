using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;

namespace Woodman.Cheats
{
    [EcsSystem(typeof(CheatsModule))]
    public class DebugTestSystem : IRunSystem
    {
        private DataWorld _world;
        public void Run()
        {
            var q = _world.Select<DebugTestComponent>();
            ref var debug = ref q.SelectFirst<DebugTestComponent>();
            debug.d += Time.deltaTime;
            debug.f -= Time.deltaTime;
        }
    }
}