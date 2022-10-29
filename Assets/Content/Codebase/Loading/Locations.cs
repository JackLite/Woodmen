using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Woodman.Loading
{
    [CreateAssetMenu(menuName = "Woodman/Locations")]
    public class Locations : ScriptableObject
    {
        public AssetReference[] locations;
        
        #if UNITY_EDITOR
        [ContextMenuItem(nameof(FillNames), nameof(FillNames))]
        #endif
        public string[] names;

        #if UNITY_EDITOR
        public void FillNames()
        {
            names = new string[locations.Length];
            for (var i = 0; i < locations.Length; i++)
            {
                var assetReference = locations[i];
                names[i] = assetReference.editorAsset.name;
            }
        }
        #endif
    }
}