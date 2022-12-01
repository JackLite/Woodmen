using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Common.Delay;
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

            var state = ev.buildingView.GetState(ev.newState);
            ev.buildingView.ShowBuildingVFX(_poolsProvider.BuildingFxPool, state);
            ev.buildingView.TriggerBuildAnimation();
            ev.buildingView.ToggleProgress(false);
            TransparencyDown(ev);

            q.DestroyAll();
        }

        private void BlinkUp(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState);
            var settings = _visual.buildingSettings;
            stateView.ToggleBlink(true);
            var tween = new TweenData
            {
                remain = settings.blinkTime,
                update = r =>
                {
                    var blink = (settings.blinkTime - r) / settings.blinkTime * settings.blinkValue;
                    stateView.SetBlink(blink);
                },
                validate = () => ev.buildingView != null,
                onEnd = () =>
                {
                    ev.onFinishBuilding?.Invoke();
                    stateView.ToggleBlink(false);
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void TransparencyDown(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState - 1);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.transparencyDownTime,
                update = r =>
                {
                    var transparency = (settings.transparencyDownTime - r) / settings.transparencyDownTime * settings.transparencyValue;
                    stateView.SetTransparency(settings.transparencyValue - transparency);
                },
                validate = () => ev.buildingView != null,
                onEnd = () =>
                {
                    ev.buildingView.GetState(ev.newState).SetTransparency(0);
                    ev.buildingView.SetState(ev.newState);
                    ev.buildingView.SetLogs(0, ev.nextStateLogs);
                    DelayedTransparencyUp(ev);
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void DelayedTransparencyUp(BuildingChangeStateEvent ev)
        {
            var settings = _visual.buildingSettings;
            DelayedFactory.Create(_world, settings.delay, () => TransparencyUp(ev));
        }

        private void TransparencyUp(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.transparencyUpTime,
                update = r =>
                {
                    var transparency = (settings.transparencyUpTime - r) / settings.transparencyUpTime * settings.transparencyValue;
                    stateView.SetTransparency(transparency);
                },
                validate = () => ev.buildingView != null,
                onEnd = () =>
                {
                    ev.buildingView.HideVfx(_poolsProvider.BuildingFxPool);
                    ev.buildingView.ResetBuildAnimation();
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