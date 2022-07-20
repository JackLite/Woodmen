using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePieceFactory
    {
        private readonly TreeContainer _treeContainer;
        private Quaternion _benchRotation = Quaternion.Euler(0, 0, 90);
        public TreePieceFactory(TreeContainer treeContainer)
        {
            _treeContainer = treeContainer;
        }

        public TreePiece Create(Side side, int num, bool hasBench, bool isShort)
        {
            var tree = Object.Instantiate(_treeContainer.TreePrefab, new Vector3(0, num, 0), Quaternion.identity);
            tree.IsHasBench = hasBench;
            if (!hasBench)
                return tree;
            tree.Side = side;
            var pos = GetPosForBench(num, isShort, side == Side.Right);
            var prefab = isShort ? _treeContainer.ShortBenchPrefab : _treeContainer.LongBenchPrefab;

            Object.Instantiate(prefab, pos, _benchRotation, tree.transform);
            return tree;
        }

        private static Vector2 GetPosForBench(int num, bool isShort, bool isRight)
        {
            var x = isShort ? 1 : 1.5f;
            x = isRight ? x : -x;
            return new Vector3(x, num * 1 + 1, 0);
        }
    }
}