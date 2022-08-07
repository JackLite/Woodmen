using UnityEngine;
using Woodman.PlayerRes;

namespace Woodman.Common
{
    public class MetaUiProvider : MonoBehaviour
    {
        [field: SerializeField]
        public ResourceBarMetaUI ResourceBarMetaUI { get; private set; }

    }
}