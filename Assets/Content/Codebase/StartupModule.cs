using System.Reflection;
using System.Threading.Tasks;
using EcsCore;
using UnityEngine;
using Woodman.Buildings;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Logs;
using Woodman.MetaTrees;
using Woodman.Player;
using Woodman.Utils;

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
            AddDependency(new LogsHeapRepository());
            var viewProvider = Object.FindObjectOfType<MainViewProvider>(true);
            AddDependency(viewProvider);
            BindView(viewProvider);
            CreateOneData<DebugStateData>();
            return Task.CompletedTask;
        }
    }
}