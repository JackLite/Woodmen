using System;
using UnityEngine;
using Woodman.Utils;

namespace Woodman.Felling.Tree.Branches
{
    public class BranchView : MonoBehaviour
    {
        [SerializeField]
        private BoosterView[] _boosters;

        [SerializeField]
        private BranchElementView _hive;

        [SerializeField]
        private GameObject _longBranch;

        [SerializeField]
        private GameObject _shortBranch;

        public event Action<BoosterType> OnBoosterCollide;
        public event Action OnHiveCollide;

        private void Awake()
        {
            _hive.OnPlayerCollide += () => OnHiveCollide?.Invoke();
            foreach (var booster in _boosters)
            {
                booster.OnPlayerCollide += () =>
                {
                    OnBoosterCollide?.Invoke(booster.BoosterType);
                    booster.Destroy();
                };
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

        public void ActivateHive()
        {
            _hive.gameObject.SetActive(true);
        }

        public void Revert()
        {
            transform.localScale = transform.localScale.MirrorX();
        }
    }
}