using UnityEngine;
using UnityEngine.UI;
using Woodman.Common;

namespace Woodman.Settings
{
    public class SettingsWindow : SimpleUiWindow
    {
        [SerializeField]
        private Button closeBtn;

        private void Awake()
        {
            closeBtn.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }
}