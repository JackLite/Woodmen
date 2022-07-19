using System;
using UnityEngine;
    
namespace Movement
{
    /// <summary>
    /// Enter point for read movement input
    /// </summary>
    public class ControlMovementPlayer : MonoBehaviour
    {
        [SerializeField]
        private ReaderPlayerMovement _reader;

        [SerializeField]
        private CircleMovementPlayer _movementCircle;

        public event Action OnStopMove;

        private bool _isMoveActive;
        private Vector2 _input;

        private void Awake()
        {
            _reader.OnChangeMoveState += OnChangeMoveState;
        }

        public Vector2 ReadInput()
        {
            return _input;
        }

        private void OnChangeMoveState(bool isActive)
        {
            if (isActive)
                _movementCircle.SetStartPosition(_reader.CurrentPointerPos);
            else
            {
                _input = Vector2.zero;
                _movementCircle.ResetToDefault();
                OnStopMove?.Invoke();
            }
            _isMoveActive = isActive;
        }

        private void Update()
        {
            if (!_isMoveActive)
                return;

            _input = _movementCircle.Delta;
            _movementCircle.SetPosition(_reader.CurrentPointerPos);
        }
    }
}