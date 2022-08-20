using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePieceFactory
    {
        private readonly TreeContainer _treeContainer;

        public TreePieceFactory(TreeContainer treeContainer)
        {
            _treeContainer = treeContainer;
        }

        public TreePiece Create(Vector3 rootPos, FellingSide fellingSide, int num, bool hasBranch, bool isShort)
        {
            var piecePos = new Vector3(rootPos.x, rootPos.y + num * TreeConstants.PieceHeight, rootPos.z);
            var tree = Object.Instantiate(_treeContainer.TreePrefab, piecePos, Quaternion.identity);
            tree.name = $"Tree piece - {fellingSide}/{hasBranch} - {num}";
            tree.IsHasBranch = hasBranch;
            if (!hasBranch)
                return tree;
            tree.FellingSide = fellingSide;
            var branchPos = GetPosForBranch(rootPos, num, isShort, fellingSide == FellingSide.Right);
            var prefab = isShort ? _treeContainer.ShortBenchPrefab : _treeContainer.LongBenchPrefab;

            Object.Instantiate(prefab, branchPos, GetRotationForBranch(fellingSide), tree.transform);
            return tree;
        }

        private static Vector3 GetPosForBranch(Vector3 rootPos, int num, bool isShort, bool isRight)
        {
            var x = 0.5f;
            x = isRight ? x : -x;
            return new Vector3(rootPos.x + x, rootPos.y + num * TreeConstants.PieceHeight + 1, rootPos.z);
        }

        private static Quaternion GetRotationForBranch(FellingSide side)
        {
            return side == FellingSide.Right ? Quaternion.Euler(0, -180, 0) : Quaternion.identity;
        }
    }
}