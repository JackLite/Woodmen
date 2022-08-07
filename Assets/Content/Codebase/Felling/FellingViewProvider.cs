using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Utils;

namespace Woodman.Felling
{
    public class FellingViewProvider : ViewProvider
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