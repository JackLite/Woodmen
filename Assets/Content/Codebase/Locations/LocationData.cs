using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Woodman.Locations
{
    public struct LocationData
    {
        public AssetReference currentLocation;
        public LocationView locationView;
        public SceneInstance currentLocationScene;
    }
}