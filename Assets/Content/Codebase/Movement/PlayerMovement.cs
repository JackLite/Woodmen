using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        public void Move(Vector2 delta)
        {
            Debug.Log(delta);
        }
    }
}