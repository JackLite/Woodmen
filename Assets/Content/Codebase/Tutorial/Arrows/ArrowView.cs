using ModulesFrameworkUnity;
using UnityEngine;
using Woodman.Common.Tweens;

namespace Woodman.Tutorial.Arrows
{
    public class ArrowView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rect;

        [SerializeField]
        private float _time = 1f;

        [SerializeField]
        private AnimationCurve _easing;

        [SerializeField]
        private float _deltaY = 50f;

        private void MoveDown()
        {
            var startPos = _rect.anchoredPosition;
            var endPos = startPos - _deltaY * Vector2.down;
            var tween = new TweenData
            {
                remain = _time,
                update = r =>
                {
                    var normalized = (_time - r) / _time;
                    var f = _easing.Evaluate(normalized);
                    _rect.anchoredPosition = Vector2.Lerp(startPos, endPos, f);
                },
                validate = () => _rect != null && isActiveAndEnabled,
                onEnd = MoveUp
            };
            EcsWorldContainer.World.NewEntity().AddComponent(tween);
        }

        private void MoveUp()
        {
            var startPos = _rect.anchoredPosition;
            var endPos = startPos + _deltaY * Vector2.down;
            var tween = new TweenData
            {
                remain = _time,
                update = r =>
                {
                    var normalized = (_time - r) / _time;
                    var f = _easing.Evaluate(normalized);
                    _rect.anchoredPosition = Vector2.Lerp(startPos, endPos, f);
                },
                validate = () => _rect != null && isActiveAndEnabled,
                onEnd = MoveDown
            };
            EcsWorldContainer.World.NewEntity().AddComponent(tween);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            MoveDown();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}