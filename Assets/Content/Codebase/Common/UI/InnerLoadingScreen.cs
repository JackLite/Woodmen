using UnityEngine;

namespace Woodman.Common.UI
{
    /// <summary>
    /// View of switching screen between core and meta
    /// </summary>
    public class InnerLoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        
        public float animDuration = 2f;
        private static readonly int HideWindow = Animator.StringToHash("HideWindow");

        public void Show()
        {
            _animator.SetBool(HideWindow, false);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _animator.SetBool(HideWindow, true);
        }
    }
}