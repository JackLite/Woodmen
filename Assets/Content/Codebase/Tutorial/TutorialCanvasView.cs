using UnityEngine;
using Woodman.Common;

namespace Woodman.Tutorial
{
    public class TutorialCanvasView : SimpleUiWindow
    {
        [SerializeField]
        private GameObject fingerContainer;

        public void ToggleFinger(bool state)
        {
            fingerContainer.SetActive(state);
        }
    }
}