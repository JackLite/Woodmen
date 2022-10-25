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

        public void Init(AssetReference[] locations)
        {
            foreach (var location in locations)
            {
                var btn = Instantiate(_locationChoseBtnPrefab, _btnsParent).GetComponent<LocationChoseBtn>();
                btn.SetName(location.editorAsset.name);
                btn.location = location;
                btn.OnOnLocationChosen += r => OnOnLocationChosen?.Invoke(r);
            }
        }
    }
}