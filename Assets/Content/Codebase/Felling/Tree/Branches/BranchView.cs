using System;
using UnityEngine;

namespace Woodman.Felling.Tree.Branches
{
    public class BranchView : MonoBehaviour
    {
        [SerializeField]
        private BoosterView[] _boosters;

        [SerializeField]
        private GameObject _longBranch;

        [SerializeField]
        private GameObject _shortBranch;

        public event Action<BoosterType> OnBoosterCollide;
        
        private void Awake()
        {
            foreach (var booster in _boosters)
            {
                booster.OnPlayerCollide += () => OnBoosterCollide?.Invoke(booster.BoosterType);
            }
        }

        public void MakeShort()
        {
            _longBranch.SetActive(false);
            _shortBranch.SetActive(true);
        }

        public void ActivateBooster(BoosterType boosterType)
        {
            foreach (var booster in _boosters)
            {
                booster.gameObject.SetActive(boosterType == booster.BoosterType);
            }
        }

        public void RevertBoosters()
        {
            foreach (var booster in _boosters)
            {
                booster.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}