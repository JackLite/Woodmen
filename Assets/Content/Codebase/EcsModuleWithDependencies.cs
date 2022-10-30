using System;
using System.Collections.Generic;
using System.Reflection;
using ModulesFramework.Modules;
using UnityEngine;
using Woodman.Utils;

namespace Woodman
{
    public abstract class EcsModuleWithDependencies : EcsModule
    {
        private readonly Dictionary<Type, object> _dependencies = new();

        public override Dictionary<Type, object> GetDependencies()
        {
            return _dependencies;
        }

        protected void AddDependency<T>(T d) where T : class
        {
            AddDependency(typeof(T), d);
        }

        private void AddDependency(Type t, object d)
        {
            _dependencies[t] = d;
        }
        
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

                AddDependency(p.FieldType, val);
            }
        }
    }
}