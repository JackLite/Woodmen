using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Loading
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        [SerializeField]
        private TMP_Text _loadProgress;

        [SerializeField]
        private TMP_Text _id;

        private void Awake()
        {
            _id.text = SystemInfo.deviceUniqueIdentifier.Substring(0, 25);
        }

        public void SetProgress(float p)
        {
            _slider.value = p;
            _loadProgress.text = Mathf.CeilToInt(p * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}