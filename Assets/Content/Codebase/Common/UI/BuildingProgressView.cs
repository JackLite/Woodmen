using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Common.UI
{
    public class BuildingProgressView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private Slider _slider;

        private int _current;
        private int? _total;

        public void Init(int current, int total)
        {
            _current = current;
            _total = total;
            UpdateView();
        }

        public void SetProgress(int current, int total)
        {
            if (_total == null)
                Logger.LogError(nameof(BuildingProgressView), nameof(SetProgress), "Not initialized");
            _current = current;
            _total = total;
            UpdateView();
        }

        private void UpdateView()
        {
            if (_total != null)
            {
                _slider.value = (float)_current / _total.Value;
                _text.text = $"{(_total - _current).ToString()}";
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}