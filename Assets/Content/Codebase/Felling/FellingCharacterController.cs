using System.Collections.Generic;
using UnityEngine;

namespace Woodman.Felling
{
    public class FellingCharacterController : MonoBehaviour
    {
        private static readonly int _cut = Animator.StringToHash("Attack");
        private static readonly int _dead = Animator.StringToHash("Dead");

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
                Switch();
            CurrentFellingSide = fellingSide;
        }

        private void Switch()
        {
            var rot = _character.rotation.eulerAngles;
            _character.rotation = Quaternion.Euler(rot.x, rot.y + 180, rot.z);
        }

        public void Cut()
        {
            _animator.SetTrigger(_cut);
        }

        public void Dead()
        {
            _animator.SetBool(_dead, true);
        }

        public void ResetDead()
        {
            _animator.SetBool(_dead, false);
        }
    }
}