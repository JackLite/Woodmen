using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Misc;

namespace Woodman.Felling
{
    public class FellingViewProvider : MonoBehaviour
    {
        [field:SerializeField]
        [field:ViewInject]
        public TreeContainer TreeContainer { get; private set; }
        
        [field:SerializeField]
        [field:ViewInject]
        public FellingCharacterController FellingCharacterController { get; private set; }
        
        [field:SerializeField]
        [field:ViewInject]
        public FellingUIProvider FellingUIProvider { get; private set; }
    }
}