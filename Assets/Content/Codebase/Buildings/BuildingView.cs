using System;
using UnityEngine;
using UnityEngine.Assertions;
using Logger = Woodman.Utils.Logger;

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
        private int[] _logsCount;

        [SerializeField]
        private Animator _animator;

        private static readonly int BuildingTrigger = Animator.StringToHash("Building");

        private void Awake()
        {
            SetState(0); // todo: load from save
        }

        private void GenerateGuid()
        {
            if (string.IsNullOrWhiteSpace(_guid))
                _guid = Guid.NewGuid().ToString();
        }

        public int GetResForState(int stateIndex)
        {
            if (stateIndex < 0 || stateIndex >= _states.Length)
            {
                Logger.LogError(nameof(BuildingView),
                    nameof(SetState),
                    $"Wrong index: {stateIndex}. Logs count: {_logsCount.Length}. Object: {gameObject.name}");
                return int.MaxValue;
            }

            return _logsCount[stateIndex];
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
    }
}