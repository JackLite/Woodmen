using System;
using UnityEngine;

namespace MetaInteractions
{

    public class InteractTarget : MonoBehaviour
    {
        [SerializeField]
        private float _interactionDelay = 1;

        [field:SerializeField]
        public InteractTypeEnum InteractType { get; private set; }

        private bool _isInteract;
        private float _startInteractionTime;

        public event Action OnStartInteract;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            _startInteractionTime = Time.time + _interactionDelay;
            _isInteract = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player"))
                return;

            _isInteract = false;
        }

        private void Update()
        {
            if (!_isInteract)
                return;

            if (Time.time > _startInteractionTime)
            {
                OnStartInteract?.Invoke();
                _isInteract = false;
            }
        }
    }
}