using TMPro;
using UnityEngine;

namespace Woodman.Common.UI
{
    public class CountText : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private int _current;

        public void SetCount(int count)
        {
            _current = count;
            _text.text = _current.ToString();
        }
    }
}