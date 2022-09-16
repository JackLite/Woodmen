using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Cheats.View
{
    public class DebugResourceViewProvider : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_InputField LogsInput { get; private set; }

        [field: SerializeField]
        public Button LogsApplyBtn { get; private set; }
        
        [field: SerializeField]
        public Button CloseBtn { get; private set; }
    }
}