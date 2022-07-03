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

        public event Action<Vector2> OnChange;

        private bool _isMoveActive;

        private void Awake()
        {
            _reader.OnChangeMoveState += OnChangeMoveState;
        }

        private void OnChangeMoveState(bool isActive)
        {
            if (isActive)
                _movementCircle.SetStartPosition(_reader.CurrentPointerPos);
            else
                _movementCircle.ResetToDefault();
            _isMoveActive = isActive;
        }

        private void Update()
        {
            if (!_isMoveActive)
                return;

            _movementCircle.SetPosition(_reader.CurrentPointerPos);
            OnChange?.Invoke(_movementCircle.Delta);
        }
    }
}