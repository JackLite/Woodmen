using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Player.Indicators
{
    public class TreeInteractIndicator : MonoBehaviour
    {
        [SerializeField]
        private Image _slider;

        private void Awake()
        {
            _slider.fillAmount = 0;
        }

        public void Toggle(bool state)
        {
            gameObject.SetActive(state);
            if (state == false)
                _slider.fillAmount = 0;
        }

        public void SetProgress(float p)
        {
            _slider.fillAmount = p;
        }

        public bool IsActive() => gameObject.activeSelf;
    }
}