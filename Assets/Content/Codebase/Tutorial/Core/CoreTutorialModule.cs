using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Tutorial.Meta;

namespace Woodman.Tutorial.Core
{
    public class CoreTutorialModule : TutorialModule
    {
        private GameObject _canvas;

        protected override async Task Setup()
        {
            await base.Setup();
            _canvas = await Addressables.InstantiateAsync("TutorialCanvas").Task;

            var view = _canvas.GetComponent<TutorialViewProvider>();
            AddDependency(view);
            BindView(view);
            view.TutorialCanvasView.Hide();
        }

        public override void OnDestroy()
        {
            Addressables.Release(_canvas);
        }
    }
}