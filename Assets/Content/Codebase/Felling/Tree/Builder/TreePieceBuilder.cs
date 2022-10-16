using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePieceBuilder
    {
        private readonly TreeContainer _treeContainer;
        private TreePiece _currentPiece;

        public TreePieceBuilder(TreeContainer treeContainer)
        {
            _treeContainer = treeContainer;
        }

        public TreePieceBuilder Create(Vector3 rootPos, FellingSide fellingSide, int num)
        {
            var piecePos = new Vector3(rootPos.x, rootPos.y + num * TreeConstants.PieceHeight, rootPos.z);
            _currentPiece = Object.Instantiate(_treeContainer.TreePrefab, piecePos, Quaternion.identity);
            _currentPiece.name = $"Tree piece - {fellingSide} - {num}";
            _currentPiece.BranchSide = fellingSide;
            return this;
        }

        public TreePieceBuilder SetBranch(bool isShort)
        {
            _currentPiece.IsHasBranch = true;
            var branchPos = GetPosForBranch(_currentPiece.BranchSide == FellingSide.Right);
            var prefab = isShort ? _treeContainer.ShortBenchPrefab : _treeContainer.LongBenchPrefab;

            Object.Instantiate(prefab, branchPos, GetRotationForBranch(_currentPiece.BranchSide),
                _currentPiece.transform);
            return this;
        }

        public TreePiece Flush()
        {
            var piece = _currentPiece;
            _currentPiece = null;
            return piece;
        }

        private Vector3 GetPosForBranch(bool isRight)
        {
            var x = 0.5f;
            x = isRight ? x : -x;
            var piecePos = _currentPiece.transform.position;
            return new Vector3(piecePos.x + x, piecePos.y + 1, piecePos.z);
        }

        private static Quaternion GetRotationForBranch(FellingSide side)
        {
            return side == FellingSide.Right ? Quaternion.Euler(0, -180, 0) : Quaternion.identity;
        }

        public TreePieceBuilder SetType(TreePieceType type)
        {
            _currentPiece.Size = type == TreePieceType.Hollow ? 2 : 1;
            _currentPiece.SetType(type);
            return this;
        }
    }
}