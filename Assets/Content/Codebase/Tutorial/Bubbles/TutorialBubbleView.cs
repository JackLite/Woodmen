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

        [SerializeField]
        private RectTransform _bubble;

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
            _bubble.pivot = side == FellingSide.Left ? _leftPivot : _rightPivot;
            _leftArrow.SetActive(side == FellingSide.Left);
            _rightArrow.SetActive(side == FellingSide.Right);
        }

        public void SetBubbleAnchor(Vector3 position)
        {
            _bubble.anchoredPosition = position + Vector3.down * 50;
        }
    }
}