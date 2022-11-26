using UnityEngine;
using Woodman.Felling.Taps.CutFx;
using Woodman.Felling.Tree;
using Woodman.Utils;

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
        public FellingPositions FellingPositions { get; private set; }

        [field:SerializeField]
        [field:ViewInject]
        public CutFxPool CutFxPool { get; private set; }
        
        [field:SerializeField]
        [field:ViewInject]
        public CutTextPool CutTextPool { get; private set; }
        
        [field:SerializeField]
        public Transform Environment { get; private set; }
    }
}