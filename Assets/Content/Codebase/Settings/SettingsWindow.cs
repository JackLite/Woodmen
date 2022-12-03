using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Settings
{
    public class SettingsWindow : SimpleUiWindow
    {
        [SerializeField]
        private Button closeBtn;
        
        [SerializeField]
        private Button _privacyBtn;

        [SerializeField]
        private string _privacyUrl;

        private void Awake()
        {
            closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
            _privacyBtn.onClick.AddListener(() =>
            {
                Application.OpenURL(_privacyUrl);
            });
        }
    }
}