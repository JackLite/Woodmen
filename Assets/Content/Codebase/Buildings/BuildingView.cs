using System;
using System.Threading.Tasks;
#if UNITY_EDITOR
using ModulesFrameworkUnity;
using UnityEditor;
#endif
using UnityEngine;
using Woodman.Common.UI;
using Woodman.Utils;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Buildings
{
    public class BuildingView : MonoBehaviour
    {
        [ContextMenuItem("Generate guid", nameof(GenerateGuid))]
        [SerializeField]
        private string _guid;

        [SerializeField]
        private BuildingStateView[] _states;

        [SerializeField]
        private int _totalLogsCount;

        [ContextMenuItem("Recalc", nameof(CalculateStateLogsCount))]
        [SerializeField]
        private int[] _logsCount;

        [SerializeField]
        private BuildingProgressView _progress;

        [SerializeField]
        private Transform _vfxParent;

        [SerializeField]
        private Animator _animator;
        
        private float _framesPerSecond;

        private int _currentCount;
        private Transform _currentBuildingVfx;
        private static readonly int Building = Animator.StringToHash("Building");

        public string Id => _guid;
        public int StatesCount => _states.Length;

        private void Awake()
        {
            _progress.Init(0, _logsCount[1]);
        }

        private void GenerateGuid()
        {
            if (string.IsNullOrWhiteSpace(_guid))
                _guid = Guid.NewGuid().ToString();
            #if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
            EditorUtility.SetDirty(this);
            #endif
        }

        public void SetLogs(int count, int total)
        {
            _currentCount = count;
            _progress.SetProgress(_currentCount, total);
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
                state.ToggleActive(i == index);
            }

            if (_states.Length - 1 == index)
            {
                _progress.Hide();
            }
        }

        public void ToggleProgress(bool state)
        {
            if (state)
                _progress.Show();
            else
                _progress.Hide();
        }

        public bool IsLastState(int state)
        {
            return state >= StatesCount - 1;
        }

        public void ShowBuildingVFX(BuildingFxPool vfxPool)
        {
            _currentBuildingVfx = vfxPool.Get();
            _currentBuildingVfx.SetParent(_vfxParent);
            _currentBuildingVfx.localPosition = Vector3.zero;
            _currentBuildingVfx.localRotation = Quaternion.identity;
            _currentBuildingVfx.gameObject.SetActive(false);
            _currentBuildingVfx.gameObject.SetActive(true);
        }

        public void HideVfx(BuildingFxPool vfxPool)
        {
            vfxPool.Return(_currentBuildingVfx);
        }

        public void TriggerBuildAnimation()
        {
            _animator.SetTrigger(Building);
        }

        public void ResetBuildAnimation()
        {
            _animator.ResetTrigger(Building);
        }

        public BuildingStateView GetState(int state)
        {
            return _states[state];
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Show building effect")]
        private void ShowBuildingEffect()
        {
            var newState = 2;
            var oldState = GetState(newState - 1);
            oldState.SetTransparency(1);
            oldState.ToggleActive(true);
            GetState(newState).ToggleActive(false);

            EcsWorldContainer.World.CreateEvent(new BuildingChangeStateEvent()
            {
                buildingView = this,
                newState = 3,
                nextStateLogs = 0,
                onFinishBuilding = () => { }
            });
        }
        #endif
    }
}