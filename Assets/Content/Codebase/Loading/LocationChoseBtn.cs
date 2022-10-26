using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Woodman.Loading
{
    [RequireComponent(typeof(Button))]
    public class LocationChoseBtn : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;
        
        public AssetReference location;
        public event Action<AssetReference> OnOnLocationChosen;
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => OnOnLocationChosen?.Invoke(location));
        }

        public void SetName(string text)
        {
            _name.text = text;
        }
    }
}