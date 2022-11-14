﻿using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common.LifetimeData;

namespace Woodman.Common.Delay
{
    [EcsSystem(typeof(StartupModule))]
    public class DelayedSystem : IRunSystem
    {
        private DataWorld _world;
        
        public void Run()
        {
            var q = _world.Select<DelayData>()
                .Without<Lifetime>();
            foreach (var e in q.GetEntities())
            {
                ref var dd = ref e.GetComponent<DelayData>();
                if (dd.validate != null)
                {
                    if(dd.validate())
                        dd.delayedFun?.Invoke();
                }
                else
                {
                    dd.delayedFun?.Invoke();
                }
                e.Destroy();
            }
        }
    }
}