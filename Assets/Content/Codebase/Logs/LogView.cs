using UnityEngine;

namespace Woodman.Logs
{
    public class LogView : MonoBehaviour
    {
        [field: Header("For debug")]
        [field: SerializeField]
        public int Count { get; set; }

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