using UnityEngine;

namespace Woodman.Common
{
    /// <summary>
    /// Общий класс для всех полноэкранных окон
    /// </summary>
    public abstract class SimpleUiWindow : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}