using UnityEngine;

namespace Woodman.Player.Movement.View
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int _stopWalk = Animator.StringToHash("StopWalk");
        private static readonly int _startWalk = Animator.StringToHash("StartWalk");

        [SerializeField]
        private float _speed;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        private Vector2 _lastDelta;
        public bool IsMoving => _rigidbody.velocity != Vector3.zero;

        public void Move(Vector2 delta)
        {
            var direction = new Vector3(delta.x, 0, delta.y);
            _rigidbody.AddForce(direction * _speed, ForceMode.Acceleration);
            UpdateAnimation(delta);

            if (delta == Vector2.zero)
                return;

            _character.rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
        }

        public void StopMove()
        {
            _lastDelta = Vector2.zero;
            _rigidbody.AddForce(Vector3.zero, ForceMode.Acceleration);
            _animator.SetTrigger(_stopWalk);
        }

        private void UpdateAnimation(Vector2 delta)
        {
            if (_lastDelta != Vector2.zero && delta == Vector2.zero)
                _animator.SetTrigger(_stopWalk);
            else if (_lastDelta == Vector2.zero && delta != Vector2.zero)
                _animator.SetTrigger(_startWalk);
            _lastDelta = delta;
        }
    }
}