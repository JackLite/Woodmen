using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Player;
using Woodman.Settings;

namespace Woodman.Meta
{
    [EcsSystem(typeof(MetaModule))]
    public class TransparencySystem : IActivateSystem, IRunSystem, IDeactivateSystem
    {
        private DataWorld _world;
        private PlayerVisibilityDetector _visibilityDetector;
        private VisualSettings _visualSettings;
        
        public void Activate()
        {
            _visibilityDetector.OnVisible += OnPlayerVisible;
            _visibilityDetector.OnHided += OnPlayerHided;
        }

        public void Run()
        {
            var entities = _world.Select<Transparency>().GetEntities();
            foreach (var e in entities)
            {
                ref var t = ref e.GetComponent<Transparency>();
                if (t.isFinish) continue;
                var delta = t.delta * Time.deltaTime;
                var needFinish = false;
                if (delta > 0)
                    needFinish = t.current + delta > t.target;
                else if (delta < 0)
                    needFinish = t.current + delta < t.target;

                if (needFinish)
                {
                    t.transparent.SetTransparency(t.target);
                    if (delta < 0)
                        t.isFinish = true;
                    else 
                        e.Destroy();
                    continue;
                }
                t.current += t.delta * Time.deltaTime;
                t.transparent.SetTransparency(t.current);
            }
        }

        public void Deactivate()
        {
            _visibilityDetector.OnVisible -= OnPlayerVisible;
            _visibilityDetector.OnHided -= OnPlayerHided;
        }

        private void OnPlayerVisible()
        {
            var q = _world.Select<Transparency>().GetEntities();
            foreach (var e in q)
            {
                ref var t = ref e.GetComponent<Transparency>();
                t.delta = _visualSettings.transparencySpeed;
                t.isFinish = false;
                t.target = 1;
            }
        }

        private void OnPlayerHided(GameObject obj)
        {
            var meshTransparent = obj.GetComponent<MeshTransparent>();
            if (meshTransparent == null)
            {
                Debug.LogError(obj.name + " does not have " + nameof(MeshTransparent) + "component");
                return;
            }

            _world.NewEntity().AddComponent(new Transparency
            {
                target = _visualSettings.min,
                current = 1,
                delta = -_visualSettings.transparencySpeed,
                transparent = meshTransparent
            });
        }
    }
}