using System.Threading.Tasks;
using EcsCore;
using UnityEngine;
using Woodman.Common;
using Woodman.EcsCodebase.Player;

namespace Woodman.EcsCodebase
{
    [GlobalModule]
    public class StartupModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            CreateOneData(new PlayerOneData {maxWoodCount = 50});
            dependencies[typeof(MainViewProvider)] = Object.FindObjectOfType(typeof(MainViewProvider));
            return Task.CompletedTask;
        }
    }
}