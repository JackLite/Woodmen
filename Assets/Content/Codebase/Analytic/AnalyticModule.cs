using System.Threading.Tasks;
using ModulesFramework.Attributes;
using Woodman.Utils;

namespace Woodman.Analytic
{
    [GlobalModule]
    public class AnalyticModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            AddDependency(new AnalyticSenderFacade());
            return Task.CompletedTask;
        }
    }
}