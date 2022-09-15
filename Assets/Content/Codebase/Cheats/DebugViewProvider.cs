using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Cheats
{
    public class DebugViewProvider : MonoBehaviour
    {
        [field:SerializeField]
        public Button ShowDebugPanelBtn { get; private set; }
        
        [field:SerializeField]
        public Button ResetSaveBtn { get; private set; }
        
        [field:SerializeField]
        public GameObject DebugPanel { get; private set; }
    }
}