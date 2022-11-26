using ModulesFramework.Attributes;
using ModulesFramework.Systems;

namespace Woodman.Tutorial.Meta
{
    [EcsSystem(typeof(MetaTutorialModule))]
    public class TutorialShowSystem : IActivateSystem
    {
        private TutorialCanvasView _tutorialCanvas;

        public void Activate()
        {
            _tutorialCanvas.Show();
        }
    }
}