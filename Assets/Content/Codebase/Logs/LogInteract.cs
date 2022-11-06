using UnityEngine;
using Woodman.Locations.Interactions;

namespace Woodman.Logs
{
    public class LogInteract : InteractTarget
    {
        [field: SerializeField]
        public LogView LogView { get; private set; }
    }
}