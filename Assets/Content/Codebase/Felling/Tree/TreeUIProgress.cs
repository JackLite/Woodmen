using UnityEngine;
using UnityEngine.UI;

namespace Woodman.Felling.Tree
{
    /// <summary>
    ///     Отображение прогресса рубки в коре
    /// </summary>
    public class TreeUIProgress : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        /// <param name="p">From 0 to 1</param>
        public void SetProgress(float p)
        {
            _slider.value = p * _slider.maxValue;
        }
    }
}