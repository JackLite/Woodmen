using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;
using Woodman.Felling;

namespace Woodman.Tutorial.Bubbles
{
    public class TutorialBubbleView : SimpleUiWindow
    {
        [SerializeField]
        private TMP_Text _text;

        public RectTransform bubble;

        [SerializeField]
        private GameObject _leftArrow;

        [SerializeField]
        private GameObject _rightArrow;

        [SerializeField]
        private Vector2 _leftPivot;

        [SerializeField]
        private Vector2 _rightPivot;

        [SerializeField]
        private Button _button;

        public float Height => bubble.rect.height;
        
        public event Action OnBubbleClick;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnBubbleClick?.Invoke());
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetBubbleSide(FellingSide side)
        {
            bubble.pivot = side == FellingSide.Left ? _leftPivot : _rightPivot;
            _leftArrow.SetActive(side == FellingSide.Left);
            _rightArrow.SetActive(side == FellingSide.Right);
        }

        public void SetBubbleAnchor(Vector2 position)
        {
            bubble.anchoredPosition = position;
        }

        public void ResetBubbleAnchor()
        {
            bubble.anchorMin = Vector2.zero;
            bubble.anchorMax = Vector2.zero;
        }

        public void ToggleInteract(bool interactable)
        {
            _button.interactable = interactable;
        }
    }
}