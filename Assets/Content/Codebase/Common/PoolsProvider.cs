using UnityEngine;
using Woodman.Buildings;

namespace Woodman.Common
{
    public class PoolsProvider : MonoBehaviour
    {
        [field:SerializeField]
        public BuildingFxPool BuildingFxPool { get; private set; }
    }
}