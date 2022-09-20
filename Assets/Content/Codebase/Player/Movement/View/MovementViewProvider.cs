using UnityEngine;

namespace Woodman.Player.Movement.View
{
    public class MovementViewProvider : MonoBehaviour
    {
        [field:SerializeField]
        public ReaderPlayerMovement Reader { get; private set; }

        [field:SerializeField]
        public CircleMovementPlayer CircleMovement { get; private set; }
    }
}