using UnityEngine;
using Woodman.Common.UI;

namespace Woodman.Logs
{
    public class LogView : MonoBehaviour
    {
        [SerializeField]
        private CountText _countText;

        [SerializeField]
        private int _count;

        //todo: завести репозиторий для куч брёвен
        public int Count => _count;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetCount(int count)
        {
            _count = count;
            _countText.SetCount(_count);
        }

        public void Subtract(int count)
        {
            _count -= count;
            _countText.SetCount(_count);
            if (_count <= 0)
                Hide();
        }
    }
}