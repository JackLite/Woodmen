using UnityEngine;
using Woodman.Player.Movement.View;
using Woodman.PlayerRes;

namespace Woodman.Common
{
    public class MetaUiProvider : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceBarMetaUI ResourceBarMetaUI { get; private set; }
        
        [field: SerializeField]
        public MovementViewProvider MovementViewProvider { get; private set; }
    }
}