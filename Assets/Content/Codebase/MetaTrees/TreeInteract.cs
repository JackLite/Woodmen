using UnityEngine;
using Woodman.MetaInteractions;

namespace Woodman.MetaTrees
{
    public class TreeInteract : InteractTarget
    {
        [field: SerializeField]
        public TreeMeta TreeMeta { get; private set; }
    }
}