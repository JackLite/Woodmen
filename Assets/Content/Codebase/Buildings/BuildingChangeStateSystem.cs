using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Common.Tweens;
using Woodman.Settings;

namespace Woodman.Buildings
{
    [EcsSystem(typeof(MetaModule))]
    public class BuildingChangeStateSystem : IRunSystem
    {
        private DataWorld _world;
        private PoolsProvider _poolsProvider;
        private VisualSettings _visual;

        public void Run()
        {
            var q = _world.Select<BuildingChangeStateEvent>();
            if (!q.TrySelectFirst(out BuildingChangeStateEvent ev))
                return;

            ev.buildingView.ShowBuildingVFX(_poolsProvider.BuildingFxPool);
            ev.buildingView.ToggleProgress(false);
            TransparencyDown(ev);

            q.DestroyAll();
        }

        private void BlinkUp(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.blinkTime,
                update = r =>
                {
                    var blink = (settings.blinkTime - r) / settings.blinkTime * 1;
                    stateView.SetBlink(blink);
                },
                validate = () => ev.buildingView != null,
                onEnd = ev.onFinishBuilding
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void TransparencyDown(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState - 1);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.transparencyTime,
                update = r =>
                {
                    var transparency = (settings.transparencyTime - r) / settings.transparencyTime * 2;
                    stateView.SetTransparency(2 - transparency);
                },
                validate = () => ev.buildingView != null,
                onEnd = () =>
                {
                    ev.buildingView.GetState(ev.newState).SetTransparency(0);
                    ev.buildingView.SetState(ev.newState);
                    ev.buildingView.SetLogs(0, ev.nextStateLogs);
                    TransparencyUp(ev);
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void TransparencyUp(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.transparencyTime,
                update = r =>
                {
                    var transparency = (settings.transparencyTime - r) / settings.transparencyTime * 2;
                    stateView.SetTransparency(transparency);
                },
                validate = () => ev.buildingView != null,
                onEnd = () =>
                {
                    ev.buildingView.HideVfx(_poolsProvider.BuildingFxPool);
                    if (ev.buildingView.IsLastState(ev.newState))
                        BlinkUp(ev);
                    else
                    {
                        ev.onFinishBuilding?.Invoke();
                        ev.buildingView.ToggleProgress(true);
                    }
                }
            };
            _world.NewEntity().AddComponent(tween);
        }
    }
}