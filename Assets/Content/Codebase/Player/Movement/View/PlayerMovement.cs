using UnityEngine;

namespace Woodman.Player.Movement.View
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int StopWalk = Animator.StringToHash("StopWalk");
        private static readonly int StartWalk = Animator.StringToHash("StartWalk");

        [SerializeField]
        private float _speed;

        [SerializeField]
        [Range(0, 1)]
        private float _inputThreshold = .4f;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private float _animationSpeedFactor = 1;

        private Vector2 _lastDelta;
        public bool IsMoving => _rigidbody.velocity != Vector3.zero;
        public Vector3 CurrentPos => transform.position;

        public void Move(Vector2 delta)
        {
            var mg = delta.magnitude;
            if(mg < _inputThreshold)
                delta = Vector2.zero;
            var direction = new Vector3(delta.x, 0, delta.y);
            _rigidbody.AddForce(direction * _speed, ForceMode.Acceleration);
            UpdateAnimation(delta);
            if (delta == Vector2.zero)
            {
                _animator.speed = 1;
                return;
            }
            _animator.speed = delta.magnitude * _animationSpeedFactor;

            _character.rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
        }

        public void StopMove()
        {
            _lastDelta = Vector2.zero;
            _rigidbody.AddForce(Vector3.zero, ForceMode.Acceleration);
            _animator.SetTrigger(StopWalk);
        }

        private void UpdateAnimation(Vector2 delta)
        {
            if (_lastDelta != Vector2.zero && delta == Vector2.zero)
            {
                _animator.ResetTrigger(StartWalk);
                _animator.SetTrigger(StopWalk);
            }
            else if (_lastDelta == Vector2.zero && delta != Vector2.zero)
            {
                _animator.ResetTrigger(StopWalk);
                _animator.SetTrigger(StartWalk);
            }

            _lastDelta = delta;
        }
    }
}