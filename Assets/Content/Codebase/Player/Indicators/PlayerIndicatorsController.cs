using UnityEngine;

namespace Woodman.Player.Indicators
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