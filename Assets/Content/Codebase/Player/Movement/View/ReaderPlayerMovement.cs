using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Woodman.Player.Movement.View
{
    public class ReaderPlayerMovement : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        private bool _isMoveStarted;
        public Vector2 CurrentPointerPos { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isMoveStarted = true;
            CurrentPointerPos = eventData.pressPosition;
            OnChangeMoveState?.Invoke(_isMoveStarted);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (!_isMoveStarted)
                return;
            CurrentPointerPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Stop move!");
            _isMoveStarted = false;
            OnChangeMoveState?.Invoke(_isMoveStarted);
        }

        public event Action<bool> OnChangeMoveState;
    }
}