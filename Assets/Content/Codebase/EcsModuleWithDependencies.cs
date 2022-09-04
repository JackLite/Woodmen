using System;
using System.Collections.Generic;
using EcsCore;

namespace Woodman
{
    public abstract class EcsModuleWithDependencies : EcsModule
    {
        protected readonly Dictionary<Type, object> dependencies = new();

        public override Dictionary<Type, object> GetDependencies()
        {
            return dependencies;
        }

        protected void AddDependency<T>(T d) where T : class
        {
            dependencies[typeof(T)] = d;
        }
    }
}