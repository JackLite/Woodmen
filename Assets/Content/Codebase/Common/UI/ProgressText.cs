using TMPro;
using UnityEngine;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Common.UI
{
    public class ProgressText : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private int _current;
        private int? _total;

        public void Init(int current, int total)
        {
            _current = current;
            _total = total;
            UpdateText();
        }

        public void SetProgress(int current, int total)
        {
            if (_total == null)
                Logger.LogError(nameof(ProgressText), nameof(SetProgress), "Not initialized");
            _current = current;
            _total = total;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = $"{_current.ToString()}/{_total.ToString()}";
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