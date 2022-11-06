using System;
using ModulesFramework;
using ModulesFramework.Data;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Felling.Settings;
using Woodman.Felling.Tree.Branches;
using Woodman.Felling.Tree.Builder;
using Woodman.Meta;
using Random = UnityEngine.Random;

namespace Woodman.Felling.Tree.Generator
{
    public class TreeGenerator
    {
        private readonly TreePieceBuilder _pieceBuilder;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly TreePieceTypeGenerator _typeGenerator;
        private readonly DataWorld _world;
        private readonly TreeGenerationSettings _settings;
        private readonly TreePieceBranchModGenerator _branchModGenerator;
        private NextBranchData _nextBranch;

        private float _branchSP;
        private float _branchSPAcc;

        public TreeGenerator(
            TreePieceBuilder pieceBuilder,
            TreePiecesRepository treePiecesRepository, 
            DataWorld world,
            TreeGenerationSettings settings)
        {
            _settings = settings;
            _pieceBuilder = pieceBuilder;
            _treePiecesRepository = treePiecesRepository;
            _world = world;
            _typeGenerator = new TreePieceTypeGenerator(settings);
            _branchModGenerator = new TreePieceBranchModGenerator(settings);
            _branchSP = settings.branchSwitching.min;
            _branchSPAcc = settings.branchSwitching.minAcc;
        }

        public GameObject Generate(Vector3 rootPos, int size)
        {
            Reset();
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
                
                var side = GenerateSide(pieceIndex, prevSide);
                var builder = _pieceBuilder.Create(rootPos + Vector3.up * 0.5f, side, pieceIndex)
                        .SetType(type);

                if (pieceIndex > 4)
                    ProcessBranch(builder, pieceIndex,  side, type, ref branchBias, ref prevSide);

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

        private void Reset()
        {
            _typeGenerator.Reset();
            _branchModGenerator.Reset();
            _branchSP = _settings.branchSwitching.min;
            _branchSPAcc = _settings.branchSwitching.minAcc;
        }

        private FellingSide GenerateSide(int pieceIndex, FellingSide? prevSide)
        {
            var settings = _settings.branchSwitching;
            var accDelta = settings.accDelta * settings.accMultiplier * (pieceIndex / settings.accMod);
            _branchSPAcc = math.clamp(_branchSPAcc + accDelta, settings.minAcc, settings.maxAcc);
            _branchSP = math.clamp(_branchSP + _branchSPAcc, settings.min, settings.max);
            
            var isChange = Random.Range(0, 1f) < _branchSP;
            if (!isChange)
                return prevSide ?? (Random.Range(0, 1f) > .5f ? FellingSide.Left : FellingSide.Right);
            _branchSPAcc = settings.minAcc;
            _branchSP = settings.min;
            return prevSide == FellingSide.Left ? FellingSide.Right : FellingSide.Left;
        }

        private void ProcessBranch(
            TreePieceBuilder builder, 
            int pieceIndex, 
            FellingSide side,
            TreePieceType pieceType,
            ref int branchBias, 
            ref FellingSide? prevSide)
        {
            if (_nextBranch != null)
            {
                var branch = CreateBranch(builder);
                if (_nextBranch.pieceType == TreePieceType.Strong)
                    branch.MakeStrong();
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
                        pieceType = pieceType
                    };
                    branchBias++;
                }
                else
                {
                    var branch = CreateBranch(builder);
                    if (pieceType == TreePieceType.Strong)
                        branch.MakeStrong();
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
            public BranchModEnum mod;
            public TreePieceType pieceType;
        }
    }
}