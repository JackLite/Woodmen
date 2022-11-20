using System.Globalization;
using TMPro;
using UnityEngine;

namespace Woodman.Felling.Taps.CutFx
{
    public class CutTextView : MonoBehaviour
    {
        [Range(0, 1)]
        public float endOpacity;

        public float angleAmplitude;
        public float xAmplitude;
        public float endY;

        [SerializeField]
        private TMP_Text _text;

        public void SetCount(int count)
        {
            _text.text = $"+{count.ToString(CultureInfo.InvariantCulture)}";
        }

        public void SetOpacity(float o)
        {
            _text.alpha = o;
        }
    }
}