using UnityEngine;

namespace Woodman.Felling.Tree.Branches
{
    public class BoosterView : BranchElementView
    {
        [field: SerializeField]
        public BoosterType BoosterType { get; private set; }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}