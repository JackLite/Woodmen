using UnityEngine;
using Woodman.Player.Movement.View;
using Woodman.Player.PlayerResources;
using Woodman.Utils;

namespace Woodman.Meta
{
    public class MetaUiProvider : MonoBehaviour
    {
        [field: SerializeField]
        [field: ViewInject]
        public ResourceBarMetaUI ResourceBarMetaUI { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public MovementViewProvider MovementViewProvider { get; private set; }
    }
}