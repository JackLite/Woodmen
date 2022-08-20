using System;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling.Win
{
    public class FellingWinWindow : SimpleUiWindow
    {
        [SerializeField]
        private Button _okBtn;

        private void Awake()
        {
            _okBtn.onClick.AddListener(() => OnOkBtnClick?.Invoke());
        }

        public event Action OnOkBtnClick;
    }
}