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
            var pieceIndex = 0;
            var s = size;
            while (s > 0)
            {
                var isRight = Random.Range(0, 1f) > .5f;
                var side = isRight ? FellingSide.Right : FellingSide.Left;

                var type = GenerateType(pieceIndex);
                if (type != TreePieceType.Hollow) 
                    --s;
                var builder = _pieceBuilder.Create(rootPos + Vector3.up * 0.5f, side, pieceIndex)
                    .SetType(type);
                
                var hasBranch = pieceIndex % 2 == 0 && pieceIndex >= 4;
                if (hasBranch)
                {
                    var branch = builder.CreateBranch();
                    var isShort = Random.Range(0, 1f) > .5f;
                    if (isShort)
                        branch.MakeShort();
                    branch.OnBoosterCollide += CreateBoosterEvent;
                    branch.OnHiveCollide += CreateHiveEvent;
                    var r = Random.Range(0, 1f);
                    if (r is > .15f and < .25f)
                        branch.ActivateBooster(BoosterType.TimeFreeze);
                    else if(r is > .25f and < .35f)
                        branch.ActivateBooster(BoosterType.RestoreTime);
                    else if (r > .35f)
                        branch.ActivateHive();
                }

                var tree = builder.Flush();
                tree.transform.SetParent(parent.transform, true);
                _treePiecesRepository.AddPiece(tree);
                ++pieceIndex;
            }

            return _treePiecesRepository.GetBottomPiece().gameObject;
        }

        private void CreateHiveEvent()
        {
            _world.NewEntity().AddComponent(new HiveCollideEvent());
        }

        private static TreePieceType GenerateType(int pieceIndex)
        {
            var type = TreePieceType.Usual;
            var typeR = Random.Range(0, 1f);
            if (pieceIndex > 10 && typeR is > .5f and < .75f)
                type = TreePieceType.Strong;
            else if (pieceIndex > 20 && typeR > .75f)
                type = TreePieceType.Hollow;
            return type;
        }

        private void CreateBoosterEvent(BoosterType boosterType)
        {
            _world.NewEntity().AddComponent(new BoosterCollideEvent { boosterType = boosterType });
        }
    }
}