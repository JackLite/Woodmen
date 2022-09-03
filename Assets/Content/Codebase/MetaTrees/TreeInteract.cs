using UnityEngine;
using Woodman.EcsCodebase.MetaInteractions;

namespace Woodman.MetaTrees
{
    public class TreeInteract : InteractTarget
    {
        [field: SerializeField]
        public TreeMeta TreeMeta { get; private set; }
    }
}