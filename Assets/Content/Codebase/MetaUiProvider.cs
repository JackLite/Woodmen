using UnityEngine;
using Woodman.PlayerRes;

namespace Woodman
{
    public class MetaUiProvider : MonoBehaviour
    {
        [field:SerializeField]
        public ResourceBarMetaUI ResourceBarMetaUI { get; private set; }
    }
}