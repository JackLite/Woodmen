using System;
using Core;
using UnityEngine;
using Woodman.Felling.Settings;
using Woodman.Felling.Tree.Branches;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Tree.Generator
{
    public class TreeGenerator
    {
        private readonly TreePieceBuilder _pieceBuilder;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly TreePieceTypeGenerator _typeGenerator;
        private readonly DataWorld _world;
        private readonly FellingSettings _fellingSettings;
        private readonly TreePieceBranchModGenerator _branchModGenerator;
        private NextBranchData _nextBranch;

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
            _branchModGenerator = new TreePieceBranchModGenerator(settings);
        }

        public GameObject Generate(Vector3 rootPos, int size)
        {
            _typeGenerator.Reset();
            _branchModGenerator.Reset();
            var parent = new GameObject("TreeCore");
            var pieceIndex = 0;
            var s = size;
            FellingSide? prevSide = null;
            var branchBias = 0;
            while (s > 0)
            {
                var type = _typeGenerator.Generate(pieceIndex);
                if (type  == TreePieceType.Usual)
                    --s;
                
                var isRight = Random.Range(0, 1f) > .5f;
                var side = isRight ? FellingSide.Right : FellingSide.Left;
                var builder = _pieceBuilder.Create(rootPos + Vector3.up * 0.5f, side, pieceIndex)
                        .SetType(type);

                ProcessBranch(builder, pieceIndex, ref branchBias, side, ref prevSide);

                var tree = builder.Flush();
                if (type == TreePieceType.Strong)
                {
                    var strongSize = _typeGenerator.GenerateStrongSize();
                    tree.SetSize(strongSize);
                    s -= strongSize;
                }

                tree.transform.SetParent(parent.transform, true);
                _treePiecesRepository.AddPiece(tree);
                ++pieceIndex;
            }

            return _treePiecesRepository.GetBottomPiece().gameObject;
        }

        private void ProcessBranch(TreePieceBuilder builder, int pieceIndex, ref int branchBias, FellingSide side,
            ref FellingSide? prevSide)
        {
            if (_nextBranch != null)
            {
                var branch = CreateBranch(builder);
                prevSide = side;

                switch (_nextBranch.mod)
                {
                    case BranchModEnum.TimeFreeze:
                        branch.ActivateBooster(BoosterType.TimeFreeze);
                        break;
                    case BranchModEnum.RestoreTime:
                        branch.ActivateBooster(BoosterType.RestoreTime);
                        break;
                    case BranchModEnum.Hive:
                        branch.ActivateHive();
                        break;
                    case BranchModEnum.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _nextBranch = null;
                return;
            }
            var hasBranch = (pieceIndex + branchBias) % 2 == 0 && pieceIndex >= 4;
            if (hasBranch)
            {
                var isSameSide = prevSide.HasValue && prevSide == side;
                var branchMod = _branchModGenerator.GenerateBranchMod(pieceIndex, isSameSide);

                if (branchMod != BranchModEnum.None)
                {
                    _nextBranch = new NextBranchData
                    {
                        mod = branchMod,
                        side = side
                    };
                    branchBias++;
                }
                else
                {
                    CreateBranch(builder);
                    prevSide = side;
                }
            }
        }

        private BranchView CreateBranch(TreePieceBuilder builder)
        {
            var branch = builder.CreateBranch();
            var isShort = Random.Range(0, 1f) > .5f;
            if (isShort)
                branch.MakeShort();
            branch.OnBoosterCollide += CreateBoosterEvent;
            branch.OnHiveCollide += CreateHiveEvent;
            return branch;
        }

        private void CreateHiveEvent()
        {
            _world.NewEntity().AddComponent(new HiveCollideEvent());
        }

        private void CreateBoosterEvent(BoosterType boosterType)
        {
            _world.NewEntity().AddComponent(new BoosterCollideEvent { boosterType = boosterType });
        }

        private class NextBranchData
        {
            public FellingSide side;
            public BranchModEnum mod;
        }
    }
}