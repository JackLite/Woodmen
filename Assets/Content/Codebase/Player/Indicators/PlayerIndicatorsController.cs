using UnityEngine;

namespace Woodman.Player.Indicators
{
    public class PlayerIndicatorsController : MonoBehaviour
    {
        [SerializeField]
        private GameObject treeInteractIndicator;

        [SerializeField]
        private GameObject backpackLog;

        public void ToggleTreeIndicator(bool state)
        {
            treeInteractIndicator.SetActive(state);
        }

        public void ToggleLogBackpack(bool state)
        {
            backpackLog.SetActive(state);
        }
    }
}