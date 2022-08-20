using System;
using UnityEngine;
using Zenject;

namespace Woodman.Felling.Timer
{
    public class FellingTimer : ITickable
    {
        private readonly FellingUIProvider _uiProvider;
        private bool _isActive;
        private float _remainTime;
        private float _totalTime;

        public FellingTimer(FellingUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void Tick()
        {
            if (!_isActive)
                return;

            _remainTime -= Time.deltaTime;
            if (_remainTime < 0)
            {
                _remainTime = 0;
                _isActive = false;
                OnEnd?.Invoke();
            }

            _uiProvider.FellingTimerView.SetProgress(_remainTime / _totalTime);
        }

        public event Action OnEnd;

        public void Start(float time)
        {
            _isActive = true;
            _totalTime = time;
            _remainTime = time;
        }

        public void AddTime(float time)
        {
            _remainTime = Mathf.Min(_remainTime + time, _totalTime);
        }

        public void Stop()
        {
            _isActive = false;
        }
    }
}