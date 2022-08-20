using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling.Lose
{
    public class FellingLoseWindow : SimpleUiWindow
    {
        [SerializeField]
        private Button _homeBtn;

        public event Action OnHomeClick;

        public void Awake()
        {
            _homeBtn.onClick.AddListener(() => OnHomeClick?.Invoke());
        }
    }
}