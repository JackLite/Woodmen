using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling
{
    public class FellingUi : SimpleUiWindow
    {
        [SerializeField]
        private Button _pauseBtn;

        public GameObject timer;
        public GameObject progress;
        
        public event Action OnPause;

        private void Awake()
        {
            _pauseBtn.onClick.AddListener(() => OnPause?.Invoke());
        }
    }
}