using UnityEngine;

namespace Woodman.Buildings
{
    public class BuildingInteract : MonoBehaviour
    {
        public int tempStateIndex;

        [field: SerializeField]
        public BuildingView BuildingView { get; private set; }
    }
}