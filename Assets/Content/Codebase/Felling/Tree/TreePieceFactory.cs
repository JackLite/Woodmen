using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePieceFactory
    {
        private readonly TreeContainer _treeContainer;
        private readonly Quaternion _benchRotation = Quaternion.Euler(0, 0, 90);
        public TreePieceFactory(TreeContainer treeContainer)
        {
            _treeContainer = treeContainer;
        }

        public TreePiece Create(Vector3 rootPos, Side side, int num, bool hasBench, bool isShort)
        {
            var piecePos = new Vector3(rootPos.x, rootPos.y + num, rootPos.z);
            var tree = Object.Instantiate(_treeContainer.TreePrefab, piecePos, Quaternion.identity);
            tree.name = $"Tree piece - {side}/{hasBench} - {num}";
            tree.IsHasBench = hasBench;
            if (!hasBench)
                return tree;
            tree.Side = side;
            var benchPos = GetPosForBench(rootPos, num, isShort, side == Side.Right);
            var prefab = isShort ? _treeContainer.ShortBenchPrefab : _treeContainer.LongBenchPrefab;

            Object.Instantiate(prefab, benchPos, _benchRotation, tree.transform);
            return tree;
        }

        private static Vector3 GetPosForBench(Vector3 rootPos, int num, bool isShort, bool isRight)
        {
            var x = isShort ? 1 : 1.5f;
            x = isRight ? x : -x;
            return new Vector3(rootPos.x + x, rootPos.y + num * 1 + 1, rootPos.z);
        }
    }
}