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
        private MeshRenderer _longBranch;

        [SerializeField]
        private MeshRenderer _shortBranch;

        [SerializeField]
        private Material _strongMat;

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
            _longBranch.gameObject.SetActive(false);
            _shortBranch.gameObject.SetActive(true);
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

        public bool IsHasBooster()
        {
            foreach (var booster in _boosters)
            {
                if (booster.gameObject.activeSelf)
                    return true;
            }

            return false;
        }

        public BoosterType GetBoosterType()
        {
            foreach (var booster in _boosters)
            {
                if (booster.gameObject.activeSelf)
                    return booster.BoosterType;
            }

            return BoosterType.Undefined;
        }

        public bool IsHasHive()
        {
            return _hive.gameObject.activeSelf;
        }

        public BranchElementView GetActiveElement()
        {
            foreach (var booster in _boosters)
            {
                if (booster.gameObject.activeSelf)
                    return booster;
            }

            if (_hive.gameObject.activeSelf)
                return _hive;
            return null;
        }

        public void Revert()
        {
            transform.localScale = transform.localScale.MirrorX();
        }

        public void MakeStrong()
        {
            _longBranch.material = _strongMat;
            _shortBranch.material = _strongMat;
        }
    }
}