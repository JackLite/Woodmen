using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;

namespace Woodman.Logs.LogsUsing
{
    [EcsSystem(typeof(MetaModule))]
    public class UsingLogsMoveSystem : IRunSystem
    {
        private DataWorld _world;
        private LogsUsingPool _pool;
        private VisualSettings _visualSettings;
        
        public void Run()
        {
            var q = _world.Select<UsingLogs>();
            foreach (var entity in q.GetEntities())
            {
                ref var d = ref entity.GetComponent<UsingLogs>();
                d.remain -= Time.deltaTime;
                
                if (d.remain <= 0)
                {
                    d.log.position = d.to;
                    _pool.Return(d.log);
                    entity.Destroy();
                    continue;
                }

                var lerpFactor = 1 - d.remain / d.time;
                var y = _visualSettings.usingLogsYEasing.Evaluate(lerpFactor) * _visualSettings.usingLogsYMax;
                var newPos = Vector3.Lerp(d.from, d.to, lerpFactor);
                
                d.log.position = new Vector3(newPos.x, newPos.y + y, newPos.z);
                d.log.Rotate(_visualSettings.usingLogsRotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }
}