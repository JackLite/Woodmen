using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Cheats.View
{
    public class GodModeBtn : MonoBehaviour
    {
        [SerializeField]
        private Button _btn;

        [SerializeField]
        private TMP_Text _text;

        public event Action onClick;

        private void Awake()
        {
            _btn.onClick.AddListener(() => onClick?.Invoke());
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}