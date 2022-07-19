using UnityEngine;

namespace Woodman.Felling
{
    public class FellingViewProvider : MonoBehaviour
    {
        [field:SerializeField]
        public TreeContainer TreeContainer { get; private set; }
        
        [field:SerializeField]
        public FellingPlayerController FellingPlayerController { get; private set; }
        
        [field:SerializeField]
        public TapController TapController { get; private set; }
    }
}