using UnityEngine;

namespace Woodman.Buildings
{
    public class BuildingInteract : MonoBehaviour
    {
        [field: SerializeField]
        public BuildingView BuildingView { get; private set; }
    }
}