using UnityEngine;

namespace Woodman.Logs
{
    public class LogView : MonoBehaviour
    {
        [field:Header("For debug")]
        [field:SerializeField]
        public int Count { get; set; }
    }
}