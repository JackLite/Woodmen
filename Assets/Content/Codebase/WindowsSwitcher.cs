using UnityEngine;

namespace Woodman
{
    public class WindowsSwitcher : MonoBehaviour
    {
        [SerializeField]
        private GameObject metaUI;

        [SerializeField]
        private GameObject coreUI;

        public void ShowHideMeta(bool state)
        {
            metaUI.SetActive(state);
        }

        public void ShowHideCore(bool state)
        {
            coreUI.SetActive(state);
        }
    }
}