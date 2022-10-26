using System;
using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling.Taps
{
    /// <summary>
    ///     Отвечает за события тапов игрока в коре
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
        
        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                OnTap?.Invoke(FellingSide.Left);
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                OnTap?.Invoke(FellingSide.Right);
        }
        #endif
    }
}