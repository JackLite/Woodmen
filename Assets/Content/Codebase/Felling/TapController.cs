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

        public event Action OnLeftTap;
        public event Action OnRightTap;

        private void Awake()
        {
            leftTap.onClick.AddListener(() => OnLeftTap?.Invoke());
            rightTap.onClick.AddListener(() => OnRightTap?.Invoke());
        }
    }
}