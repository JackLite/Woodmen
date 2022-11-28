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
        public ResourceBarUI LogsBarMetaUI { get; private set; }
        
        [field: SerializeField]
        public ResourceBarUI CoinsBarMetaUI { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public MovementView MovementView { get; private set; }

        [field: SerializeField]
        public Button SettingsButton { get; private set; }
    }
}