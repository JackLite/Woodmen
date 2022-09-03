using UnityEngine;
using Woodman.EcsCodebase.Utils;
using Woodman.Felling.Tree;

namespace Woodman.Felling
{
    public class FellingViewProvider : ViewProvider
    {
        [field: SerializeField]
        [field: ViewInject]
        public TreeContainer TreeContainer { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public FellingCharacterController FellingCharacterController { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public FellingUIProvider FellingUIProvider { get; private set; }
    }
}