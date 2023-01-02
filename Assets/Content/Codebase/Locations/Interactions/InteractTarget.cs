using System;
using UnityEngine;

namespace Woodman.Locations.Interactions
{
    public class InteractTarget : MonoBehaviour
    {
        [SerializeField]
        private float _interactionDelay = 1;

        private bool _isInteract;
        private bool _isInside;
        private float _startInteractionTime;

        [field: SerializeField]
        public InteractTypeEnum InteractType { get; private set; }

        public float Delay => _interactionDelay;

        public event Action<InteractTarget> OnStartInteract;
        public event Action<InteractTarget> OnEndInteract;
        public event Action<InteractTarget> OnInteract;

        private void Awake()
        {
            InteractionStaticPool.Register(this);
        }

        private void Update()
        {
            if (!_isInteract)
                return;

            if (Time.time > _startInteractionTime)
            {
                OnInteract?.Invoke(this);
                _isInteract = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player") || _isInside)
                return;

            _startInteractionTime = Time.time + _interactionDelay;
            _isInteract = true;
            _isInside = true;
            OnStartInteract?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            _isInteract = false;
            _isInside = false;
            OnEndInteract?.Invoke(this);
        }

        public void Disable()
        {
            InteractionStaticPool.Unregister(this);
            Destroy(this);
        }
    }
}