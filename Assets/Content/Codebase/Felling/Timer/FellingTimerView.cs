using System;
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

        private bool _isFreeze;
        private static readonly int Defroze = Animator.StringToHash("Defroze");
        private CancellationTokenSource _cts;
        private static readonly int Hide = Animator.StringToHash("Hide");

        /// <param name="p">From 0 to 1</param>
        public void SetProgress(float p)
        {
            _slider.value = p;
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
    }
}