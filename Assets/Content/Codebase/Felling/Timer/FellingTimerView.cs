using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling.Timer
{
    public class FellingTimerView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        /// <param name="p">From 0 to 1</param>
        public void SetProgress(float p)
        {
            _slider.value = p;
        }

        public void SetFreeze()
        {
            //todo: set freeze effect
        }
    }
}