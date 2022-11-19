using UnityEngine;

namespace Woodman.Player.Indicators
{
    public class PlayerIndicatorsController : MonoBehaviour
    {
        [SerializeField]
        private TreeInteractIndicator treeInteractIndicator;

        [SerializeField]
        private GameObject backpackLog;

        public void ToggleTreeIndicator(bool state)
        {
            treeInteractIndicator.Toggle(state);
        }

        public void SetTreeProgress(float p)
        {
            treeInteractIndicator.SetProgress(p);
        }

        public void ToggleLogBackpack(bool state)
        {
            backpackLog.SetActive(state);
        }

        public bool IsTreeInteractActive()
        {
            return treeInteractIndicator.IsActive();
        }
    }
}