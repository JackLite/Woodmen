using System;
using Cinemachine;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Common.Delay;
using Woodman.Common.Tweens;
using Woodman.Meta;
using Woodman.Settings;
using Woodman.Utils;

namespace Woodman.Buildings
{
    [EcsSystem(typeof(MetaModule))]
    public class BuildingChangeStateSystem : IRunSystem
    {
        private DataWorld _world;
        private MetaViewProvider _viewProvider;
        private MetaUiProvider _uiProvider;
        private PoolsProvider _poolsProvider;
        private VisualSettings _visual;

        public void Run()
        {
            var q = _world.Select<BuildingChangeStateEvent>();
            if (!q.TrySelectFirst(out BuildingChangeStateEvent ev))
                return;

            var buildingView = ev.buildingView;
            var state = buildingView.GetState(ev.newState);
            buildingView.ShowBuildingVFX(_poolsProvider.BuildingFxPool, state);
            buildingView.TriggerBuildAnimation();
            buildingView.ToggleProgress(false);

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
                    stateView.ToggleBlink(false);
                    ev.onFinishBuilding?.Invoke();
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void TransparencyDown(BuildingChangeStateEvent ev)
        {
            var stateView = ev.buildingView.GetState(ev.newState - 1);
            var settings = _visual.buildingSettings;
            TimerStatic.time = DateTime.Now;
            var tween = new TweenData
            {
                remain = settings.transparencyDownTime,
                update = r =>
                {
                    var transparency = (settings.transparencyDownTime - r) / settings.transparencyDownTime *
                                       settings.transparencyValue;
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
            var buildingView = ev.buildingView;
            var stateView = buildingView.GetState(ev.newState);
            var settings = _visual.buildingSettings;
            var tween = new TweenData
            {
                remain = settings.transparencyUpTime,
                update = r =>
                {
                    var normalizedTransparency = (settings.transparencyUpTime - r) / settings.transparencyUpTime;
                    var transparency = normalizedTransparency * settings.transparencyValue;
                    stateView.SetTransparency(transparency);
                },
                validate = () => buildingView != null,
                onEnd = () =>
                {
                    buildingView.HideVfx(_poolsProvider.BuildingFxPool);
                    buildingView.ResetBuildAnimation();
                    var diff = DateTime.Now - TimerStatic.time;
                    Debug.Log("[Animation] Building without blink takes " + diff.TotalMilliseconds + " ms");
                    if (buildingView.IsLastState(ev.newState))
                    {
                        BlinkUp(ev);
                    }
                    else
                    {
                        buildingView.ToggleProgress(true);
                        ev.onFinishBuilding?.Invoke();
                    }
                }
            };
            _world.NewEntity().AddComponent(tween);
        }
    }
}