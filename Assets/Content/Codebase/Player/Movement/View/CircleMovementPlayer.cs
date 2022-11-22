using System.Runtime.CompilerServices;
using UnityEngine;

namespace Woodman.Player.Movement.View
{
    [RequireComponent(typeof(RectTransform))]
    public class CircleMovementPlayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _selfRect;

        [SerializeField]
        private RectTransform _circle;

        private Vector2 _defaultAnchoredPosition;
        private float _diff;
        private bool _isInit;

        private Vector2 _startPos;
        public Vector2 Delta { get; private set; }
        

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            if (_isInit) 
                return;
            var p = _selfRect.position;
            _selfRect.anchorMin = Vector2.zero;
            _selfRect.anchorMax = Vector2.zero;
            _selfRect.position = p;
            _defaultAnchoredPosition = _selfRect.anchoredPosition;
            _diff = _selfRect.sizeDelta.x - _circle.sizeDelta.x;
            _isInit = true;
        }
        
        public void Toggle(bool state)
        {
            Init();
            gameObject.SetActive(state);
        }

        public void SetStartPosition(Vector2 p)
        {
            _startPos = p;
            _selfRect.anchoredPosition = p;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(Vector2 p)
        {
            var direction = p - _startPos;
            if (direction.sqrMagnitude > _diff * _diff)
            {
                _startPos += Vector2.ClampMagnitude(direction, direction.magnitude - _diff);
                _selfRect.anchoredPosition = _startPos;
            }

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
            _selfRect.anchoredPosition = _defaultAnchoredPosition;
            _circle.anchoredPosition = Vector2.zero;
        }
    }
}