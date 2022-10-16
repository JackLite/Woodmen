using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreeGenerator
    {
        private readonly TreePieceBuilder _pieceBuilder;
        private readonly TreePiecesRepository _treePiecesRepository;

        public TreeGenerator(TreePieceBuilder pieceBuilder, TreePiecesRepository treePiecesRepository)
        {
            _pieceBuilder = pieceBuilder;
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
                var type = TreePieceType.Usual;
                if (i > 10 && Random.Range(0, 1f) > .5f)
                    type = TreePieceType.Hollow;
                var builder = _pieceBuilder.Create(rootPos, side, i)
                    .SetType(type);
                if (hasBench)
                    builder.SetBranch(isShort);
                var tree = builder.Flush();
                tree.transform.SetParent(parent.transform, true);
                _treePiecesRepository.AddPiece(tree);
            }
        }
    }
}