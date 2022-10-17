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
        private Vector3 _bottomPos;
        public FellingSide CurrentFellingSide { get; private set; }

        public void SetSide(FellingSide fellingSide)
        {
            _characterContainer.position = _sideToPosMap[fellingSide];
            var rot = Quaternion.LookRotation((_bottomPos - _character.position).normalized).eulerAngles;
            _character.rotation = Quaternion.Euler(0, rot.y, 0);
            CurrentFellingSide = fellingSide;
        }

        public void InitFelling(TreeModel treeModel, GameObject bottomPiece)
        {
            _bottomPos = bottomPiece.transform.position;

            var positionY = _characterContainer.position.y;
            var leftDist = (treeModel.leftPos - treeModel.pos).magnitude;
            var rightDist = (treeModel.leftPos - treeModel.pos).magnitude;
            
            var piecePos = bottomPiece.transform.position;
            var left = Vector3.MoveTowards(piecePos, -bottomPiece.transform.right * 100, leftDist);
            var right = Vector3.MoveTowards(piecePos, bottomPiece.transform.right * 100, rightDist);
            
            var leftPos = new Vector3(left.x, positionY, left.z);
            var rightPos = new Vector3(right.x, positionY, right.z);
            _sideToPosMap[FellingSide.Left] = leftPos;
            _sideToPosMap[FellingSide.Right] = rightPos;
        }

        public void Cut()
        {
            //_animator.SetTrigger(_cut);
        }
    }
}