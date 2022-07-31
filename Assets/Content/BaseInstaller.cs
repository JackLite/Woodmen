using System.Reflection;
using UnityEngine;
using Woodman.Misc;
using Zenject;

namespace Woodman
{
    public class BaseInstaller : MonoInstaller
    {
        protected void BindView<T>(T viewProvider) where T : class
        {
            var t = typeof(T);
            foreach (var p in t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                if (p.GetCustomAttribute(typeof(ViewInjectAttribute)) == null)
                    continue;

                var val = p.GetValue(viewProvider);
                if (val == null)
                {
                    Debug.LogError($"{p.FieldType} not found in {viewProvider.GetType()}");
                    continue;
                }
                Bind(val);
            }
        }

        private void Bind(object t)
        {
            var type = t.GetType();
            Container.BindInterfacesAndSelfTo(type).FromInstance(t).AsSingle();
        }
    }
}