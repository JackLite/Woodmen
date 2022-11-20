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

        [SerializeField]
        private LogsHeapType _type;

        [SerializeField]
        private Transform _usingPoint;
        
        public int Count => _count;
        public LogsHeapType LogType => _type;
        [field:SerializeField]
        public string Id { get; set; }
        public Vector3 UsingPoint => _usingPoint.position;
        
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
        }
    }
}