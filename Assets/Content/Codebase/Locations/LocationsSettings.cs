using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Woodman.Locations
{
    [CreateAssetMenu(menuName = "Woodman/Locations")]
    public class LocationsSettings : ScriptableObject
    {
        public AssetReference[] locations;
        public bool choseLocation;

        #if UNITY_EDITOR
        [Header("EditorOnly")]
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
            EditorUtility.SetDirty(this);
        }
        #endif
    }
}