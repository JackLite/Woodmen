using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Woodman.Utils;

namespace Woodman.Tutorial
{
    public abstract class TutorialModule : EcsModuleWithDependencies
    {
        protected override async Task Setup()
        {
            var tutorialSettings = await Addressables.LoadAssetAsync<TutorialSettings>("TutorialSettings").Task;
            AddDependency(tutorialSettings);
        }
    }
}