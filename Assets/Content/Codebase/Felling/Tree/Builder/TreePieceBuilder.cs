using UnityEngine;
using Woodman.Felling.Tree.Branches;
using Woodman.Meta;

namespace Woodman.Felling.Tree.Builder
{
    public class TreePieceBuilder
    {
        private readonly TreeContainer _treeContainer;
        private readonly VisualSettings _visualSettings;
        private TreePiece _currentPiece;

        public TreePieceBuilder(TreeContainer treeContainer, VisualSettings visualSettings)
        {
            _treeContainer = treeContainer;
            _visualSettings = visualSettings;
        }

        public TreePieceBuilder Create(Vector3 rootPos, FellingSide fellingSide, int num)
        {
            var posY = rootPos.y + num * (_visualSettings.pieceHeight + _visualSettings.gap);
            var piecePos = new Vector3(rootPos.x, posY, rootPos.z) + _visualSettings.startBias;
            _currentPiece = Object.Instantiate(_treeContainer.TreePrefab, piecePos, Quaternion.identity);
            _currentPiece.name = $"Tree piece - {fellingSide} - {num}";
            _currentPiece.BranchSide = fellingSide;
            return this;
        }

        public BranchView CreateBranch()
        {
            _currentPiece.IsHasBranch = true;
            var branchPos = GetPosForBranch(_currentPiece.BranchSide == FellingSide.Right);

            var branchView = Object.Instantiate(_treeContainer.LongBranchPrefab, branchPos, Quaternion.identity,
                _currentPiece.transform);
            if (_currentPiece.BranchSide != FellingSide.Left)
            {
                branchView.Revert();
            }

            _currentPiece.Branch = branchView.gameObject;

            return branchView;
        }

        public TreePiece Flush()
        {
            var piece = _currentPiece;
            _currentPiece = null;
            return piece;
        }

        private Vector3 GetPosForBranch(bool isRight)
        {
            var x = _visualSettings.branchX;
            x = isRight ? x : -x;
            var piecePos = _currentPiece.transform.position;
            return new Vector3(piecePos.x + x, piecePos.y + _visualSettings.branchY, piecePos.z);
        }

        public TreePieceBuilder SetType(TreePieceType type)
        {
            var size = type == TreePieceType.Strong ? 2 : 1;
            _currentPiece.SetSize(size);
            _currentPiece.SetType(type);
            return this;
        }
    }
}