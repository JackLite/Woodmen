using UnityEngine;

namespace Woodman.Felling
{
    public class TreePiece : MonoBehaviour
    {
        [field:SerializeField]
        public Transform TreeMesh { get; private set; }
        
        public Side Side { get; set; }
        public bool IsHasBench { get; set; }
    }
}