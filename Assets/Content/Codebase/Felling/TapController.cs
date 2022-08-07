using System;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling
{
    /// <summary>
    /// Отвечает за события тапов игрока в коре
    /// </summary>
    public class TapController : MonoBehaviour
    {
        [SerializeField]
        private Button leftTap;

        [SerializeField]
        private Button rightTap;

        public event Action<FellingSide> OnTap;

        private void Awake()
        {
            leftTap.onClick.AddListener(() => OnTap?.Invoke(FellingSide.Left));
            rightTap.onClick.AddListener(() => OnTap?.Invoke(FellingSide.Right));
        }
    }
}