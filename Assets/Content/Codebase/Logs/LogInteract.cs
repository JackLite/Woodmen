using UnityEngine;
using Woodman.MetaInteractions;

namespace Woodman.Logs
{
    public class LogInteract : InteractTarget
    {
        [field: SerializeField]
        public LogView LogView { get; private set; }
    }
}