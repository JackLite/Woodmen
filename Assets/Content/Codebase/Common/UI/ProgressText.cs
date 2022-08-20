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
        private int _total;

        public void Init(int current, int total)
        {
            _current = current;
            _total = total;
            UpdateText();
        }

        public void SetProgress(int current)
        {
            if (_total == 0)
                Logger.LogError(nameof(ProgressText), nameof(SetProgress), "Not initialized");
            _current = current;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = $"{_current.ToString()}/{_total.ToString()}";
        }

        public void Hide()
        {
            _text.gameObject.SetActive(false);
        }
    }
}