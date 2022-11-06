using UnityEngine;
using Woodman.Locations.Interactions;

namespace Woodman.Locations.Trees
{
    public class TreeInteract : InteractTarget
    {
        [field: SerializeField]
        public TreeMeta TreeMeta { get; private set; }
    }
}