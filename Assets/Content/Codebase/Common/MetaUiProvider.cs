using UnityEngine;
using Woodman.Player.Movement.View;
using Woodman.PlayerRes;
using Woodman.Utils;

namespace Woodman.Common
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