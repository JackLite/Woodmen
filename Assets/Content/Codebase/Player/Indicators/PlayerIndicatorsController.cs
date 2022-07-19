using System;
using UnityEngine;

namespace Player.Indicators
{
    public class PlayerIndicatorsController : MonoBehaviour
    {
        [SerializeField]
        private GameObject treeInteractIndicator;

        public void ShowHideTreeIndicator(bool state)
        {
            treeInteractIndicator.SetActive(state);
        }
    }
}