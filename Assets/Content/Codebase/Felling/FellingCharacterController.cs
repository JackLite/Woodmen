using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.MetaTrees;

namespace Woodman.Felling
{
    public class FellingCharacterController : MonoBehaviour
    {
        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Animator _animator;

        private readonly Dictionary<Side, Vector3> _sideToPosMap = new();
        private static readonly int _cut = Animator.StringToHash("Cut");
        private TreeModel _currentTree;
        public Side CurrentSide { get; private set; }

        public void SetSide(Side side)
        {
            _character.position = _sideToPosMap[side];
            var rot = Quaternion.LookRotation((_currentTree.pos - _character.position).normalized).eulerAngles;
            _character.rotation = Quaternion.Euler(0, rot.y, 0);
            CurrentSide = side;
        }

        public void InitFelling(TreeModel tree)
        {
            _currentTree = tree;

            _sideToPosMap[Side.Left] = _currentTree.leftPos;
            _sideToPosMap[Side.Right] = _currentTree.rightPos;
        }

        public void Cut()
        {
            //_animator.SetTrigger(_cut);
        }
    }
}