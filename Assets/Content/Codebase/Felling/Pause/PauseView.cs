using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling.Pause
{
    /// <summary>
    /// Pause window in core gameplay
    /// </summary>
    public class PauseView : SimpleUiWindow
    {
        [SerializeField]
        private Button _playBtn;

        [SerializeField]
        private Button _restartBtn;

        public event Action OnPlay;
        public event Action OnRestart;

        private void Awake()
        {
            _playBtn.onClick.AddListener(() => OnPlay?.Invoke());
            _restartBtn.onClick.AddListener(() => OnRestart?.Invoke());
        }
    }
}