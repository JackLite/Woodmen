using UnityEngine;

namespace Woodman.Player.Movement.View
{
    public class MovementView : MonoBehaviour
    {
        [field:SerializeField]
        public ReaderPlayerMovement Reader { get; private set; }

        [field:SerializeField]
        public CircleMovementPlayer CircleMovement { get; private set; }

        public void ToggleMove(bool state)
        {
            Reader.StopMove();
            gameObject.SetActive(state);
        }
    }
}