using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
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
        private CapsuleCollider _collider;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        [Header("Ground check")]
        [SerializeField]
        private float _downForce = 9.8f;
        
        [SerializeField]
        private LayerMask _layerMask;

        [FormerlySerializedAs("_maxDistance")]
        [SerializeField]
        private float _stairsMaxDistance = 2;

        private Vector2 _lastDelta;
        private readonly RaycastHit[] _groundedHits = new RaycastHit[8];
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

            var yForce = 0f;
            if (!IsGrounded())
                yForce = _downForce;
            var normal = GetStairsNormal();
            var directionForce = new Vector3(delta.x, 0, delta.y) + normal * mg;
            var downForce = Vector3.down * yForce;
            _rigidbody.velocity = directionForce * _speed + downForce;
            _animator.SetFloat(Speed, mg);

            if (mg != 0)
                _character.rotation = Quaternion.LookRotation(new Vector3(delta.x, 0, delta.y));
        }

        private bool IsGrounded()
        {
            var r = _collider.radius;
            var counts = Physics.SphereCastNonAlloc(transform.position, r, Vector3.down, _groundedHits, 0, _layerMask);
            return counts != 0;
        }

        private Vector3 GetStairsNormal()
        {
            var r = _collider.radius;
            var counts = Physics.SphereCastNonAlloc(transform.position, r, Vector3.down, _groundedHits, _stairsMaxDistance, _layerMask);
            if (counts == 0)
                return Vector3.zero;
            var hit = _groundedHits[0];
            return hit.transform.CompareTag("Stairs") ? hit.normal : Vector3.zero;
        }
    }
}