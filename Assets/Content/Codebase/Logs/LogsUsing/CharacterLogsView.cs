using UnityEngine;

namespace Woodman.Logs.LogsUsing
{
    public class CharacterLogsView : MonoBehaviour
    {
        [SerializeField]
        private Transform _logsTarget;

        [SerializeField]
        private GameObject _logsView;

        public Vector3 LogsTargetPos => _logsTarget.position;

        public void ShowLogs()
        {
            _logsView.SetActive(true);
        }

        public void HideLogs()
        {
            _logsView.SetActive(false);
        }
    }
}