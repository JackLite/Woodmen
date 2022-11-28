using Unity.Mathematics;
using UnityEngine;
using Woodman.Utils;

namespace Woodman.Player.Movement.View
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        [Range(0, 1)]
        private float _inputThreshold = .4f;

        [SerializeField]
        [Range(0, 1)]
        private float _sensitivity = 0.2f;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        private Vector2 _lastDelta;
        private static readonly int Speed = Animator.StringToHash("Speed");
        public Vector3 CurrentPos => transform.position;

        public void Move(Vector2 delta)
        {
            var reverseSens = 1 - _sensitivity;
            var absX = math.abs(delta.x);
            var absY = math.abs(delta.y);
            if (absX > reverseSens || absY > reverseSens)
            {
                delta = delta.normalized;
            }
            else
            {
                var x = 1 - (reverseSens - delta.x) / reverseSens;
                var y = 1 - (reverseSens - delta.y) / reverseSens;
                delta = new Vector2(x, y);
            }

            var mg = delta.magnitude;
            if (mg < _inputThreshold)
            {
                _animator.SetFloat(Speed, 0);
                return;
            }

            var direction = new Vector3(delta.x, 0, delta.y);
            _rigidbody.AddForce(direction * _speed, ForceMode.Acceleration);
            _animator.SetFloat(Speed, mg);

            _character.rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
        }
    }
}