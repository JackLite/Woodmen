using System;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField]
        private RawImage _rawImage;

        [SerializeField]
        private Camera _tutorialCamera;

        public TutorialTapHand tapHand;
        public TutorialBubbleView bubbleView;

        private RenderTexture _rt;

        private void Awake()
        {
            _rt = new RenderTexture(2048, 2048, 16, RenderTextureFormat.ARGBHalf);
        }

        public void InitRenderTexture()
        {
            _rt.Create();
            _tutorialCamera.targetTexture = _rt;
            _rawImage.texture = _rt;
            _rawImage.enabled = true;
            _tutorialCamera.enabled = true;
        }

        public void ReleaseRenderTexture()
        {
            _rawImage.enabled = false;
            _tutorialCamera.enabled = false;
            _rawImage.texture = null;
            _tutorialCamera.targetTexture = null;
            _rt.Release();
        }

        private void OnDestroy()
        {
            _rt.Release();
        }

        public void ToggleMoveFinger(bool state)
        {
            fingerContainer.SetActive(state);
        }

        public void SetBubblePos(FellingSide side, Vector3 worldPos, float xOffset, float yOffset = 0)
        {
            bubbleView.SetBubbleSide(side);
            var camera = Camera.main;
            var screenPos = camera.WorldToScreenPoint(worldPos);
            screenPos += Vector3.right * xOffset;
            screenPos += Vector3.up * yOffset;
            bubbleView.SetBubbleAnchor(screenPos);
        }

        public void ShowBranchesBubble()
        {
            bubbleView.SetText("Don't get hit by branches!");
            ShowBubble();
        }

        public void ShowTimerBubble()
        {
            bubbleView.SetText("Cut down the tree before timer runs out!");
            ShowBubble();
        }

        public void ShowProgressBubble()
        {
            bubbleView.SetText("Just a little bit left");
            ShowBubble();
        }

        public void ShowFreezeBubble()
        {
            bubbleView.SetText("That thing will stop timer for a while!");
            ShowBubble();
        }

        public void ShowRestoreBubble()
        {
            bubbleView.SetText("That thing will restore timer!");
            ShowBubble();
        }

        public void ShowHiveBubble()
        {
            bubbleView.SetText("Beware of the beehive!");
            ShowBubble();
        }

        private void ShowBubble()
        {
            bubbleView.Show();
            Show();
        }
    }
}