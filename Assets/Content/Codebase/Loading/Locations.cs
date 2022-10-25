using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Woodman.Loading
{
    [CreateAssetMenu(menuName = "Woodman/Locations")]
    public class Locations : ScriptableObject
    {
        public AssetReference[] locations;
    }
}