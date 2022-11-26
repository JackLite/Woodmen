using ModulesFrameworkUnity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common.Tweens;
using Woodman.Utils;

namespace Woodman.Tutorial.Core.Taps
{
    public class TutorialTapHand : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _selfRect;

        [SerializeField]
        private Image _circle1;
        
        [SerializeField]
        private Image _circle2;

        [SerializeField]
        private RectTransform _hand;

        [SerializeField]
        private float _animationTime;
        
        [SerializeField]
        private AnimationCurve _easing;

        [SerializeField]
        private float _deltaY;

        [SerializeField]
        private float _circleScale = 2;

        public void SetPosition(Vector3 pos)
        {
            _selfRect.position = pos;
        }

        public void Toggle(bool state)
        {
            gameObject.SetActive(state);
            if (state)
                Animate();
        }

        [ContextMenu("Animate")]
        private void Animate()
        {
            _circle1.SetColorA(0);
            _circle2.SetColorA(0);
            _circle1.transform.localScale = Vector3.one * _circleScale;
            _circle2.transform.localScale = Vector3.one * _circleScale;
            MoveUp();
        }
        
        private void MoveDown()
        {
            var startPos = _hand.anchoredPosition;
            var endPos = startPos + _deltaY * Vector2.down;
            var tween = new TweenData
            {
                remain = _animationTime,
                update = r =>
                {
                    var normalized = (_animationTime - r) / _animationTime;
                    var f = _easing.Evaluate(normalized);
                    _hand.anchoredPosition = Vector2.Lerp(startPos, endPos, f);
                    _circle1.SetColorA(1 - f);
                    _circle2.SetColorA(1 - f);
                    var scale = math.lerp(1, _circleScale, f);
                    _circle1.transform.localScale = Vector3.one * scale;
                    _circle2.transform.localScale = Vector3.one * scale;
                },
                validate = () => _hand != null && isActiveAndEnabled,
                onEnd = MoveUp
            };
            EcsWorldContainer.World.NewEntity().AddComponent(tween);
        }

        private void MoveUp()
        {
            var startPos = _hand.anchoredPosition;
            var endPos = startPos - _deltaY * Vector2.down;
            var tween = new TweenData
            {
                remain = _animationTime,
                update = r =>
                {
                    var normalized = (_animationTime - r) / _animationTime;
                    var f = _easing.Evaluate(normalized);
                    _hand.anchoredPosition = Vector2.Lerp(startPos, endPos, f);
                    _circle1.SetColorA(f);
                    _circle2.SetColorA(f);
                    var scale = math.lerp(_circleScale, 1, f);
                    _circle1.transform.localScale = Vector3.one * scale;
                    _circle2.transform.localScale = Vector3.one * scale;
                },
                validate = () => _hand != null && isActiveAndEnabled,
                onEnd = MoveDown
            };
            EcsWorldContainer.World.NewEntity().AddComponent(tween);
        }
    }
}