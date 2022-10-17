using Core;
using UnityEngine;
using Woodman.Felling.Tree.Branches;

namespace Woodman.Felling.Tree
{
    public class TreeGenerator
    {
        private readonly TreePieceBuilder _pieceBuilder;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly DataWorld _world;

        public TreeGenerator(TreePieceBuilder pieceBuilder, TreePiecesRepository treePiecesRepository, DataWorld world)
        {
            _pieceBuilder = pieceBuilder;
            _treePiecesRepository = treePiecesRepository;
            _world = world;
        }

        public GameObject Generate(Vector3 rootPos, int size)
        {
            var parent = new GameObject("TreeCore");
            for (var i = 0; i < size; ++i)
            {
                var isRight = Random.Range(0, 1f) > .5f;
                var side = isRight ? FellingSide.Right : FellingSide.Left;
                var hasBranch = i % 2 == 0 && i >= 4;
                var type = TreePieceType.Usual;
                if (i > 10 && Random.Range(0, 1f) > .5f)
                    type = TreePieceType.Strong;
                var builder = _pieceBuilder.Create(rootPos + Vector3.up * 0.5f, side, i)
                    .SetType(type);
                if (hasBranch)
                {
                    var branch = builder.CreateBranch();
                    var isShort = Random.Range(0, 1f) > .5f;
                    if (isShort)
                        branch.MakeShort();
                    branch.OnBoosterCollide += CreateBoosterEvent;
                    var r = Random.Range(0, 1f);
                    if (r is > .15f and < .25f)
                        branch.ActivateBooster(BoosterType.TimeFreeze);
                    else if(r > .25f)
                        branch.ActivateBooster(BoosterType.RestoreTime);
                }

                var tree = builder.Flush();
                tree.transform.SetParent(parent.transform, true);
                _treePiecesRepository.AddPiece(tree);
            }

            return _treePiecesRepository.GetBottomPiece().gameObject;
        }

        private void CreateBoosterEvent(BoosterType boosterType)
        {
            _world.NewEntity().AddComponent(new BoosterCollideEvent { boosterType = boosterType });
        }
    }
}