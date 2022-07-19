using UnityEngine;
using Zenject;

namespace Woodman.Felling
{
    public class TreeGenerator : IInitializable
    {
        private readonly TreeContainer _container;
        private readonly PiecesController _piecesController;

        public TreeGenerator(TreeContainer container, PiecesController piecesController)
        {
            _container = container;
            _piecesController = piecesController;
        }

        public void Generate()
        {
            var size = 200;
            var benchRotation = Quaternion.Euler(0, 0, 90);
            for (var i = 0; i < size; ++i)
            {
                var tree = Object.Instantiate(_container.TreePrefab, new Vector3(0, i, 0), Quaternion.identity);
                _piecesController.AddPiece(tree);
                if (i % 2 != 0 || i < 4)
                    continue;
                tree.IsHasBench = true;
                var isRight = Random.Range(0, 1f) > .5f;
                tree.Side = isRight ? Side.Right : Side.Left;
                var isShort = Random.Range(0, 1f) > .5f;
                var pos = GetPosForBench(i, isShort, isRight);
                var prefab = isShort ? _container.ShortBenchPrefab : _container.LongBenchPrefab;

                Object.Instantiate(prefab, pos, benchRotation, tree.transform);
            }
        }

        private static Vector2 GetPosForBench(int num, bool isShort, bool isRight)
        {
            var x = isShort ? 1 : 1.5f;
            x = isRight ? x : -x;
            return new Vector3(x, num * 1 + 1, 0);
        }
        
        public void Initialize()
        {
            Generate();
        }
    }
}