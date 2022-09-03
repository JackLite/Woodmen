using UnityEngine;

namespace Woodman.EcsCodebase.Buildings
{
    public class BuildingInteract : MonoBehaviour
    {
        public int tempStateIndex;

        [field: SerializeField]
        public BuildingView BuildingView { get; private set; }
    }
}