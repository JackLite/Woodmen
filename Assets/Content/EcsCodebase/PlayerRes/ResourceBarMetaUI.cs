using System.Globalization;
using TMPro;
using UnityEngine;

namespace Woodman.EcsCodebase.PlayerRes
{
    public class ResourceBarMetaUI : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetCount(int count)
        {
            // TODO: make formatting if need
            _text.text = count.ToString(CultureInfo.InvariantCulture);
        }

        public void SetCountAnim(int count)
        {
            //TODO: make animation
        }
    }
}