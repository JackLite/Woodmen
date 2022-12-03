using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling.Finish.Lose
{
    public class FellingLoseWindow : SimpleUiWindow
    {
        [SerializeField]
        private Button _homeBtn;

        [SerializeField] 
        private Button _restartBtn;

        public event Action OnHomeClick;
        public event Action OnRestartClick;

        public void Awake()
        {
            _homeBtn.onClick.AddListener(() => OnHomeClick?.Invoke());
            _restartBtn.onClick.AddListener(() => OnRestartClick?.Invoke());
        }
    }
}