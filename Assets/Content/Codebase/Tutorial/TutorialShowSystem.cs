using ModulesFramework.Attributes;
using ModulesFramework.Systems;

namespace Woodman.Tutorial
{
    [EcsSystem(typeof(TutorialModule))]
    public class TutorialShowSystem : IActivateSystem
    {
        private TutorialCanvasView _tutorialCanvas;

        public void Activate()
        {
            _tutorialCanvas.Show();
        }
    }
}