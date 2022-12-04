using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Cheats.View
{
    public class DebugViewProvider : MonoBehaviour
    {
        [field: SerializeField]
        public Button ShowDebugPanelBtn { get; private set; }

        [field: SerializeField]
        public Button ResetSaveBtn { get; private set; }

        [field: SerializeField]
        public Button SetResourcesBtn { get; private set; }

        [field: SerializeField]
        public GameObject DebugPanel { get; private set; }

        [field: SerializeField]
        public DebugMessageView DebugMessageView { get; private set; }

        [field: SerializeField]
        public DebugResourceViewProvider DebugResourceViewProvider { get; private set; }

        [field: SerializeField]
        public GodModeBtn GodModeBtn { get; private set; }

        [field: SerializeField]
        public Button ResetCurrentLocationBtn { get; private set; }
    }
}