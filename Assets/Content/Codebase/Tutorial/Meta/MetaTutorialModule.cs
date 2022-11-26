using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Woodman.Tutorial.Meta
{
    public class MetaTutorialModule : TutorialModule
    {
        protected override async Task Setup()
        {
            await base.Setup();
            var canvas = await Addressables.InstantiateAsync("TutorialCanvas").Task;

            var view = canvas.GetComponent<TutorialViewProvider>();
            AddDependency(view);
            BindView(view);
            view.TutorialCanvasView.Hide();
        }
    }
}