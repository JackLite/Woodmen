using TMPro;
using UnityEngine;

namespace Woodman.Cheats.View
{
    public class DebugMessageView : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _text;

        public void SetMsg(string msg)
        {
            _text.text = msg;
        }
    }
}