using System;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling.Tree
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

        public event Action<Side> OnTap;

        private void Awake()
        {
            leftTap.onClick.AddListener(() => OnTap?.Invoke(Side.Left));
            rightTap.onClick.AddListener(() => OnTap?.Invoke(Side.Right));
        }
    }
}