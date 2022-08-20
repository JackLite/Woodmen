using System;
using UnityEngine;
using Woodman.Common.UI;
using Logger = Woodman.Utils.Logger;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Woodman.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [ContextMenuItem("Generate guid", nameof(GenerateGuid))]
        [SerializeField]
        private string _guid;

        [SerializeField]
        private GameObject[] _states;

        [SerializeField]
        private int _totalLogsCount;

        [ContextMenuItem("Recalc", nameof(CalculateStateLogsCount))]
        [SerializeField]
        private int[] _logsCount;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ProgressText _progress;

        private int _currentCount;
        private static readonly int BuildingTrigger = Animator.StringToHash("Building");

        public string Id => _guid;
        
        private void Awake()
        {
            SetState(0); // todo: load from save
            _currentCount = 0;
            _progress.Init(0, _logsCount[1]);
        }

        private void GenerateGuid()
        {
            if (string.IsNullOrWhiteSpace(_guid))
                _guid = Guid.NewGuid().ToString();
        }

        public void AddLogs(int count)
        {
            _currentCount += count;
            _progress.SetProgress(_currentCount);
        }
        
        public void SetLogs(int count)
        {
            _currentCount = count;
            _progress.SetProgress(_currentCount);
        }

        private void CalculateStateLogsCount()
        {
            var stateCount = _states.Length;
            var oneStateCount = _totalLogsCount / (stateCount - 1);
            var lastStateCount = oneStateCount + _totalLogsCount % oneStateCount;
            _logsCount = new int[_states.Length];
            _logsCount[0] = 0;
            for (var i = 1; i < _logsCount.Length - 1; ++i)
                _logsCount[i] = oneStateCount;
            _logsCount[^1] = lastStateCount;
            #if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
            #endif
        }

        public int GetResForState(int stateIndex)
        {
            if (stateIndex < 0 || stateIndex >= _logsCount.Length)
            {
                Logger.LogError(nameof(BuildingView),
                    nameof(SetState),
                    $"Wrong index: {stateIndex}. Logs count: {_logsCount.Length}. Object: {gameObject.name}");
                return int.MaxValue;
            }

            return _logsCount[stateIndex] - _currentCount;
        }

        public void SetState(int index)
        {
            if (index < 0 || index >= _states.Length)
            {
                Logger.LogError(nameof(BuildingView),
                    nameof(SetState),
                    $"Wrong index: {index}. States count: {_states.Length}. Object: {gameObject.name}");
                return;
            }

            for (var i = 0; i < _states.Length; ++i)
            {
                var state = _states[i];
                state.SetActive(i == index);
            }

            _animator.ResetTrigger(BuildingTrigger);
            _animator.SetTrigger(BuildingTrigger);
        }


        public void FinishBuilding()
        {
            _progress.Hide();
        }
    }
}