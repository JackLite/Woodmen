using UnityEngine;
using UnityEngine.UI;
using Woodman.Felling.Tree;

namespace Woodman.Felling
{
    public class FellingUIProvider : MonoBehaviour
    {
        [field:SerializeField]
        public TapController TapController { get; private set; }
        
        [field:SerializeField]
        public Button StartGameBtn { get; private set; }
    }
}