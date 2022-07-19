using System;
using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(RectTransform))]
    public class CircleMovementPlayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _circle;

        private RectTransform _rectTransform;
        private Vector2 _defaultAnchoredPosition;
        private Vector2 _startPos;
        private float _diff;
        public Vector2 Delta { get; private set; }

        public void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            var p = _rectTransform.position;
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.zero;
            _rectTransform.position = p;
            _defaultAnchoredPosition = _rectTransform.anchoredPosition;
            _diff = _rectTransform.sizeDelta.x - _circle.sizeDelta.x;
        }

        public void SetStartPosition(Vector2 p)
        {
            _startPos = p;
            _rectTransform.anchoredPosition = p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(Vector2 p)
        {
            var direction = p - _startPos;
            var newPos = Vector2.ClampMagnitude(direction, _diff);
            _circle.anchoredPosition = newPos;

            var max = Mathf.Max(Mathf.Abs(newPos.x), Mathf.Abs(newPos.y));
            if (max == 0)
            {
                Delta = Vector2.zero;
                return;
            }
            var delta = newPos.sqrMagnitude / (_diff * _diff);
            var x = delta * newPos.x / max;
            var y = delta * newPos.y / max;
            Delta = new Vector2(x, y);
        }

        public void ResetToDefault()
        {
            _rectTransform.anchoredPosition = _defaultAnchoredPosition;
            _circle.anchoredPosition = Vector2.zero;
        }
    }
}