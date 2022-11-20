using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling.Timer
{
    public class FellingTimerView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private Animator _freezeAnimator;

        [SerializeField]
        private float _hideAnimationTime = 1f;

        [SerializeField]
        private ColorGradient[] _colors;

        [SerializeField]
        private Image _sliderFiller;

        private ColorGradient[] _sortedColors;
        private bool _isFreeze;
        private static readonly int Defroze = Animator.StringToHash("Defroze");
        private CancellationTokenSource _cts;
        private static readonly int Hide = Animator.StringToHash("Hide");

        private void Awake()
        {
            _sortedColors = _colors.OrderBy(c => c.place).ToArray();
        }

        /// <param name="p">From 0 to 1</param>
        public void SetProgress(float p)
        {
            _slider.value = p;
            var inverted = 1 - p;
            if (Math.Abs(inverted - 1) < 0.001f)
            {
                _sliderFiller.color = _sortedColors.Last().color;
                return;
            }

            if (inverted == 0)
            {
                _sliderFiller.color = _sortedColors.First().color;
                return;
            }

            ColorGradient startedColor = default;
            ColorGradient endColor = default;
            foreach (var color in _sortedColors)
            {
                if (inverted > color.place)
                {
                    startedColor = color;
                    continue;
                }
                endColor = color;
                break;
            }
            var diff = endColor.place - startedColor.place;
            var f = (inverted - startedColor.place) / diff;
            _sliderFiller.color = Color.Lerp(startedColor.color, endColor.color, f);
        }

        public void SetFreeze()
        {
            if (_cts is { IsCancellationRequested: false })
            {
                _cts.Cancel();
            }

            if (_isFreeze)
            {
                _freezeAnimator.SetBool(Defroze, false);
                _freezeAnimator.SetBool(Hide, false);
                return;
            }

            _freezeAnimator.gameObject.SetActive(true);
        }

        public void SetDefroze()
        {
            _freezeAnimator.SetBool(Defroze, true);
        }

        public async void SetUnfreeze()
        {
            _cts = new CancellationTokenSource();
            _freezeAnimator.SetBool(Hide, true);
            await Task.Delay(TimeSpan.FromSeconds(_hideAnimationTime));
            if (_cts.IsCancellationRequested)
            {
                _cts = null;
                return;
            }

            _freezeAnimator.gameObject.SetActive(false);
            _isFreeze = false;
        }

        [Serializable]
        private struct ColorGradient
        {
            [Range(0, 1f)]
            public float place;

            public Color color;
        }
    }
}