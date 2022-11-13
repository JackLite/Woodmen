using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Felling.Win
{
    public class FellingWinWindow : SimpleUiWindow
    {
        [SerializeField]
        private TMP_Text _logsCount;
        
        [SerializeField]
        private Image _slider;

        [SerializeField]
        private TMP_Text _costText;

        [SerializeField]
        private CanvasGroup _x2Group;
        
        [SerializeField]
        private Button _okBtn;

        [SerializeField]
        private Button _x2Button;
        
        public event Action OnOkBtnClick;
        public event Action OnX2BtnClick;

        private void Awake()
        {
            _okBtn.onClick.AddListener(() => OnOkBtnClick?.Invoke());
            _x2Button.onClick.AddListener(() => OnX2BtnClick?.Invoke());
        }

        public override void Hide()
        {
            base.Hide();
            _x2Group.alpha = 1f;
            _x2Group.interactable = true;
        }

        public void SetLogsCount(int count)
        {
            _logsCount.text = count.ToString(CultureInfo.InvariantCulture);
        }

        public void SetSliderProgress(float remain)
        {
            _slider.fillAmount = remain;
        }

        public void SetCoinsCost(int coins)
        {
            _costText.text = coins.ToString();
        }

        public void HideX2()
        {
            _x2Group.alpha = .5f;
            _x2Group.interactable = false;
        }
    }
}