using UnityEngine;
using UnityEngine.UI;
using Woodman.Player.Movement.View;
using Woodman.Player.PlayerResources;
using Woodman.Utils;

namespace Woodman.Meta
{
    public class MetaUiProvider : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceBarMetaUI LogsBarMetaUI { get; private set; }
        
        [field: SerializeField]
        public ResourceBarMetaUI CoinsBarMetaUI { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public MovementViewProvider MovementViewProvider { get; private set; }

        [field: SerializeField]
        public Button SettingsButton { get; private set; }
    }
}