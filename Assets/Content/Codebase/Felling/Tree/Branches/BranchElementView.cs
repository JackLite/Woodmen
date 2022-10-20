using System;
using UnityEngine;

namespace Woodman.Felling.Tree.Branches
{
    public class BranchElementView : MonoBehaviour
    {
        public event Action OnPlayerCollide;

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.GetComponent<FellingCharacterController>() == null)
                return;

            OnPlayerCollide?.Invoke();
        }
    }
}