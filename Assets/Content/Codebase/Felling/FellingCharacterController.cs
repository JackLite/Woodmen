using System.Collections.Generic;
using UnityEngine;
using Woodman.Felling.Tree;

namespace Woodman.Felling
{
    public class FellingCharacterController : MonoBehaviour
    {
        private static readonly int _cut = Animator.StringToHash("Cut");

        [SerializeField]
        private Transform _character;

        [SerializeField]
        private Transform _characterContainer;

        [SerializeField]
        private Animator _animator;

        private readonly Dictionary<FellingSide, Vector3> _sideToPosMap = new();
        private TreeModel _currentTree;
        public FellingSide CurrentFellingSide { get; private set; }

        public void SetSide(FellingSide fellingSide)
        {
            _characterContainer.position = _sideToPosMap[fellingSide];
            var rot = Quaternion.LookRotation((_currentTree.pos - _character.position).normalized).eulerAngles;
            _character.rotation = Quaternion.Euler(0, rot.y, 0);
            CurrentFellingSide = fellingSide;
        }

        public void InitFelling(TreeModel tree)
        {
            _currentTree = tree;

            var positionY = _characterContainer.position.y;
            var leftPos = new Vector3(_currentTree.leftPos.x, positionY, _currentTree.leftPos.z);
            var rightPos = new Vector3(_currentTree.rightPos.x, positionY, _currentTree.rightPos.z);
            _sideToPosMap[FellingSide.Left] = leftPos;
            _sideToPosMap[FellingSide.Right] = rightPos;
        }

        public void Cut()
        {
            //_animator.SetTrigger(_cut);
        }
    }
}