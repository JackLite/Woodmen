using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreeGenerator
    {
        private readonly TreePieceFactory _pieceFactory;
        private readonly TreePiecesRepository _treePiecesRepository;

        public TreeGenerator(TreePieceFactory pieceFactory, TreePiecesRepository treePiecesRepository)
        {
            _pieceFactory = pieceFactory;
            _treePiecesRepository = treePiecesRepository;
        }

        public void Generate(Vector3 rootPos, int size)
        {
            var parent = new GameObject("TreeCore");
            for (var i = 0; i < size; ++i)
            {
                var isRight = Random.Range(0, 1f) > .5f;
                var side = isRight ? FellingSide.Right : FellingSide.Left;
                var isShort = Random.Range(0, 1f) > .5f;
                var hasBench = i % 2 == 0 && i >= 4;
                var tree = _pieceFactory.Create(rootPos, side, i, hasBench, isShort);
                tree.transform.SetParent(parent.transform, true);
                _treePiecesRepository.AddPiece(tree);
            }
        }
    }
}