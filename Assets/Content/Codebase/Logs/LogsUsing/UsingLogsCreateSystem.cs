using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.Delay;

namespace Woodman.Logs.LogsUsing
{
    [EcsSystem(typeof(MetaModule))]
    public class UsingLogsCreateSystem : IRunSystem
    {
        private CharacterLogsView _characterLogs;
        private DataWorld _world;
        private LogsUsingPool _pool;
        private VisualSettings _visualSettings;

        public void Run()
        {
            var q = _world.Select<UsingLogsCreateEvent>();
            if (!q.TrySelectFirst(out UsingLogsCreateEvent ev))
                return;

            for (var i = 0; i < ev.count; i++)
                DelayedFactory.Create(_world, i * ev.delayBetween, () => CreateUsingLogs(ev));

            DelayedFactory.Create(_world, ev.count * ev.delayBetween + _visualSettings.usingLogsTime, ev.onAfter);
            q.DestroyAll();
        }

        private void CreateUsingLogs(UsingLogsCreateEvent ev)
        {
            var log = _pool.Get();
            log.position = _characterLogs.LogsTargetPos;
            log.rotation = Quaternion.Euler(new Vector3(360 * Random.Range(0, 1f),
                360 * Random.Range(0, 1f),
                360 * Random.Range(0, 1f)));

            _world.NewEntity().AddComponent(new UsingLogs
            {
                log = log,
                time = _visualSettings.usingLogsTime,
                remain = _visualSettings.usingLogsTime,
                to = ev.to,
                from = ev.from
            });
        }
    }
}