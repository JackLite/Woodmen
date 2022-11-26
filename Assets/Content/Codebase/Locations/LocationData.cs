using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using Woodman.Tutorial.Meta.Arrows;

namespace Woodman.Locations
{
    public struct LocationData
    {
        public AssetReference currentLocation;
        public LocationView locationView;
        public SceneInstance currentLocationScene;
        public TutorialArrowsProvider tutorialArrowsProvider;
    }
}