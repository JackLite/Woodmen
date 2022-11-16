using UnityEngine;
using Woodman.Buildings;
using Woodman.Logs.LogsUsing;
using Woodman.Utils;

namespace Woodman.Common
{
    public class PoolsProvider : MonoBehaviour
    {
        [field:SerializeField]
        public BuildingFxPool BuildingFxPool { get; private set; }
        
        [field:SerializeField]
        [field:ViewInject]
        public LogsUsingPool LogsUsingPool { get; private set; }
    }
}