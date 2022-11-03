using System;
using System.Collections.Generic;
using UnityEngine;

namespace Woodman.Player
{
    /// <summary>
    /// Определяет видим ли персонаж игрока на данный момент
    /// </summary>
    public class PlayerVisibilityDetector : MonoBehaviour
    {
        [SerializeField]
        private CapsuleCollider _playerCollider;

        private Camera _camera;
        private bool _isHided;
        private Collider _lastHidingCollider;
        // private readonly HashSet<GameObject> _hidingColliders = new HashSet<GameObject>(8);
        // private HashSet<GameObject> _tempColliders = new HashSet<GameObject>(8);
        // private RaycastHit[] _hits = new RaycastHit[8];

        public event Action OnVisible;
        public event Action<GameObject> OnHided;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            var cameraPos = _camera.transform.position;
            var dir = _playerCollider.transform.position + _playerCollider.center - cameraPos;
            var ray = new Ray(cameraPos, dir);

            // TODO: покрыть кейс, когда 2 объекта скрывают игрока
            // var count = Physics.RaycastNonAlloc(ray, _hits);
            // if (count == 0) 
            //     return;
            //
            // Array.Sort(_hits, OrderHit);
            // var isHidedNow = false;
            // foreach (var hit in _hits)
            // {
            //     if (hit.distance == 0)
            //         continue;
            //     if (hit.collider == _playerCollider)
            //         break;
            //     isHidedNow = true;
            //     if (!_hidingColliders.Contains(hit.collider.gameObject))
            //     {
            //         _isHided = true;
            //         OnHided?.Invoke(hit.collider.gameObject);
            //         _hidingColliders.Add(hit.collider.gameObject);
            //     }
            // }
            //
            // foreach (var VARIABLE in _hidingColliders)
            // {
            //     
            // }
            //
            // if (!isHidedNow && _isHided)
            // {
            //     OnVisible?.Invoke();
            //     _isHided = false;
            //     return;
            // }
            //
            if (!Physics.Raycast(ray, out var hitInfo))
                return;
            if (hitInfo.collider == _playerCollider && _lastHidingCollider != null)
            {
                OnVisible?.Invoke();
                _lastHidingCollider = null;
            }
            else if(hitInfo.collider != _playerCollider && _lastHidingCollider != hitInfo.collider)
            {
                OnHided?.Invoke(hitInfo.collider.gameObject);
                _lastHidingCollider = hitInfo.collider;
            }
        }

        private int OrderHit(RaycastHit hit1, RaycastHit hit2)
        {
            return hit1.distance.CompareTo(hit2.distance);
        }
    }
}