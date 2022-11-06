using System.Collections.Generic;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Utils;

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
        private Vector3 _bottomPos;
        public FellingSide CurrentFellingSide { get; private set; } = FellingSide.Right;

        public void InitFelling(FellingPositions positions)
        {
            _sideToPosMap[FellingSide.Left] = positions.LeftCharPos;
            _sideToPosMap[FellingSide.Right] = positions.RightCharPos;
        }

        public void SetSide(FellingSide fellingSide)
        {
            _characterContainer.position = _sideToPosMap[fellingSide];
            if (CurrentFellingSide != fellingSide)
                SwitchScale();
            CurrentFellingSide = fellingSide;
        }

        private void SwitchScale()
        {
            _character.localScale = _character.localScale.MirrorZ();
        }

        public void Cut()
        {
            //_animator.SetTrigger(_cut);
        }
    }
}