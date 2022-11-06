using UnityEngine;

namespace Woodman.Common.UI
{
    /// <summary>
    /// View of switching screen between core and meta
    /// </summary>
    public class SwitchCoreScreen : MonoBehaviour
    {
        public float animDuration = 2f;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}