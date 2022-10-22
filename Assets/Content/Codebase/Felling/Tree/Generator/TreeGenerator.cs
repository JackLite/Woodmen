using Core;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Felling.Settings;
using Woodman.Felling.Tree.Branches;
using Woodman.Felling.Tree.Generator;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Tree
{
    public class TreeGenerator
    {
        private readonly TreePieceBuilder _pieceBuilder;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly TreePieceTypeGenerator _typeGenerator;
        private readonly DataWorld _world;
        private readonly FellingSettings _fellingSettings;

        public TreeGenerator(
            TreePieceBuilder pieceBuilder,
            TreePiecesRepository treePiecesRepository, 
            DataWorld world,
            in TreeGenerationSettings settings)
        {
            _pieceBuilder = pieceBuilder;
            _treePiecesRepository = treePiecesRepository;
            _world = world;
            _typeGenerator = new TreePieceTypeGenerator(settings);
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

                var type = _typeGenerator.Generate(pieceIndex);
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
                    /*if (r is > .15f and < .25f)
                        branch.ActivateBooster(BoosterType.TimeFreeze);
                    else if (r is > .25f and < .35f)
                        branch.ActivateBooster(BoosterType.RestoreTime);
                    else if (r > .35f)
                        branch.ActivateHive();*/
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

        private void CreateBoosterEvent(BoosterType boosterType)
        {
            _world.NewEntity().AddComponent(new BoosterCollideEvent { boosterType = boosterType });
        }
    }
}