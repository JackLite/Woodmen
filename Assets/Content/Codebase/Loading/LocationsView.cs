using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Woodman.Loading
{
    public class LocationsView : MonoBehaviour
    {
        [SerializeField]
        private LocationChoseBtn _locationChoseBtnPrefab;

        [SerializeField]
        private Transform _btnsParent;

        public event Action<AssetReference> OnOnLocationChosen;

        public void Init(AssetReference[] locations, string[] locationsNames)
        {
            for (var i = 0; i < locations.Length; i++)
            {
                var location = locations[i];
                var btn = Instantiate(_locationChoseBtnPrefab, _btnsParent).GetComponent<LocationChoseBtn>();
                btn.SetName(locationsNames[i]);
                btn.location = location;
                btn.OnOnLocationChosen += r => OnOnLocationChosen?.Invoke(r);
            }
        }
    }
}