using UnityEngine;
using Woodman.Tutorial.Arrows;
using Woodman.Tutorial.Joystick;
using Woodman.Utils;

namespace Woodman.Tutorial
{
    public class TutorialViewProvider : ViewProvider
    {
        [field: SerializeField]
        [field: ViewInject]
        public TutorialCanvasView TutorialCanvasView { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public TutorialJoystickView TutorialJoystickView { get; private set; }
    }
}