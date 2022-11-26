using UnityEngine;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Tutorial.Bubbles;
using Woodman.Tutorial.Core.Taps;

namespace Woodman.Tutorial
{
    public class TutorialCanvasView : SimpleUiWindow
    {
        [SerializeField]
        private GameObject fingerContainer;

        public TutorialTapHand tapHand;
        public TutorialBubbleView bubbleView;

        public void ToggleMoveFinger(bool state)
        {
            fingerContainer.SetActive(state);
        }

        public void SetBubblePos(FellingSide side, Vector3 worldPos, float xOffset)
        {
            bubbleView.SetBubbleSide(side);
            var camera = Camera.main;
            var screenPos = camera.WorldToScreenPoint(worldPos);
            screenPos += Vector3.right * xOffset;
            bubbleView.SetBubbleAnchor(screenPos);
        }

        public void ShowBranchesBubble()
        {
            bubbleView.SetText("Don't get hit by branches!");
            bubbleView.Show();
        }

        public void ShowTimerBubble()
        {
            bubbleView.SetText("Cut down the tree before timer runs out!");
            bubbleView.Show();
        }

        public void ShowProgressBubble()
        {
            bubbleView.SetText("Just a little bit left");
            bubbleView.Show();
        }
    }
}