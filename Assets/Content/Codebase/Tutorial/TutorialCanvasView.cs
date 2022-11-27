using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;
using Woodman.Felling;
using Woodman.Tutorial.Bubbles;
using Woodman.Tutorial.Core.Taps;
using Woodman.Utils;

namespace Woodman.Tutorial
{
    public class TutorialCanvasView : SimpleUiWindow
    {
        [SerializeField]
        private Canvas _canvas;
        
        [SerializeField]
        private GameObject fingerContainer;

        [SerializeField]
        private RawImage _rawImage;

        [SerializeField]
        private Camera _tutorialCamera;

        public RectTransform timerMask;
        public RectTransform progressMask;

        public TutorialTapHand tapHand;
        public TutorialBubbleView bubbleView;

        private RenderTexture _rt;

        private void Awake()
        {
            _tutorialCamera.transform.position = Camera.main.transform.position;
        }

        public void InitRenderTexture()
        {
            _rt = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGBHalf);
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
            _rt = null;
        }

        #if UNITY_EDITOR
        private Vector2Int _screenSize;
        private void Update()
        {
            if (_screenSize.x != Screen.width || _screenSize.y != Screen.height)
            {
                if (_rt != null)
                {
                    _tutorialCamera.transform.position = Camera.main.transform.position;
                    ReleaseRenderTexture();
                    InitRenderTexture();
                }

                _screenSize = new Vector2Int(Screen.width, Screen.height);
            }
        }
        #endif
        
        private void OnDestroy()
        {
            if (_rt != null)
                _rt.Release();
        }

        public void ToggleMoveFinger(bool state)
        {
            fingerContainer.SetActive(state);
        }

        public void SetBubblePos(FellingSide side, Vector3 worldPos, float xOffset, float yOffset = -20)
        {
            bubbleView.SetBubbleSide(side);
            var camera = Camera.main;
            var screenPos = camera.WorldToScreenPoint(worldPos);
            var pos = _canvas.ScreenToCanvasPosition(screenPos);
            pos += Vector3.right * xOffset;
            pos += Vector3.up * yOffset;
            bubbleView.ResetBubbleAnchor();
            bubbleView.SetBubbleAnchor(pos);
        }

        public void ToggleTimerMask(bool state)
        {
            timerMask.gameObject.SetActive(state);
        }
        
        public void ToggleProgressMask(bool state)
        {
            progressMask.gameObject.SetActive(state);
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