using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Utils;

namespace Woodman.Tutorial
{
    public class TutorialModule : EcsModuleWithDependencies
    {
        private GameObject _canvas;

        protected override async Task Setup()
        {
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