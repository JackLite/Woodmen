using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;
using Woodman.Felling.Finish.Lose;
using Woodman.Player.PlayerResources;

namespace Woodman.Felling.SecondChance
{
    public class SecondChanceView : SimpleUiWindow
    {
        [SerializeField]
        private ResourceBarUI _coinsBar;
        
        [SerializeField]
        private List<LoseTypeMap> _titles;

        [SerializeField]
        private TMP_Text _percent;

        [SerializeField]
        private Slider _progressSlider;

        [SerializeField]
        private Image _timeSlider;

        [SerializeField]
        private TMP_Text _cost;

        [SerializeField]
        private Button _useChanceBtn;

        [SerializeField]
        private Button _skipBtn;

        public event Action OnUseSecondChance;
        public event Action OnSkip;

        private void Awake()
        {
            _useChanceBtn.onClick.AddListener(() => OnUseSecondChance?.Invoke());
            _skipBtn.onClick.AddListener(() => OnSkip?.Invoke());
        }

        public void ActivateSkip()
        {
            _skipBtn.interactable = true;
        }

        public override void Hide()
        {
            _skipBtn.interactable = false;
            base.Hide();
        }

        public void SetLoseReason(LoseReason reason)
        {
            foreach (var typeMap in _titles)
            {
                typeMap.title.SetActive(reason == typeMap.reason);
            }
        }

        public void SetProgress(float progress)
        {
            _percent.text = ((int)(progress * 100)).ToString(CultureInfo.InvariantCulture) + "%";
            _progressSlider.value = progress;
        }

        public void SetTime(float remain, float total)
        {
            _timeSlider.fillAmount = remain / total;
        }

        public void SetCost(int cost)
        {
            _cost.text = "x" + cost.ToString(CultureInfo.InvariantCulture);
        }

        public void SetCoins(int coins)
        {
            _coinsBar.SetCoinsCount(coins);
        }

        [Serializable]
        private struct LoseTypeMap
        {
            public LoseReason reason;
            public GameObject title;
        }
    }
}