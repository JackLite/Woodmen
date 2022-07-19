using System;
using UnityEngine;

namespace Woodman.MetaInteractions
{
    public class InteractTarget : MonoBehaviour
    {
        [SerializeField]
        private float _interactionDelay = 1;

        [field:SerializeField]
        public InteractTypeEnum InteractType { get; private set; }

        private bool _isInteract;
        private float _startInteractionTime;

        public event Action<InteractTarget> OnStartInteract;
        public event Action<InteractTarget> OnEndInteract;
        public event Action<InteractTarget> OnInteract;

        private void Awake()
        {
            InteractionStaticPool.Register(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            _startInteractionTime = Time.time + _interactionDelay;
            _isInteract = true;
            OnStartInteract?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            _isInteract = false;
            OnEndInteract?.Invoke(this);
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
    }
}