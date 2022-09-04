using System.Threading.Tasks;
using EcsCore;
using UnityEngine;
using Woodman.Buildings;
using Woodman.Common;
using Woodman.MetaTrees;
using Woodman.Player;

namespace Woodman
{
    [GlobalModule]
    public class StartupModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            CreateOneData(new PlayerOneData {maxWoodCount = 50});
            AddDependency(new BuildingsRepository());
            AddDependency(new MetaTreesRepository());
            AddDependency(Object.FindObjectOfType<MainViewProvider>(true));
            return Task.CompletedTask;
        }
    }
}