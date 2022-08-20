using System.Collections.Generic;
using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiecesRepository
    {
        private readonly Queue<TreePiece> _pieces = new();

        public void AddPiece(TreePiece piece)
        {
            _pieces.Enqueue(piece);
        }

        public TreePiece GetBottomPiece()
        {
            return _pieces.Peek();
        }

        public int GetRemain()
        {
            return _pieces.Count;
        }

        public void RemovePiece()
        {
            var cutPiece = _pieces.Dequeue();
            Object.Destroy(cutPiece.gameObject);
            foreach (var piece in _pieces) piece.transform.position += Vector3.down * TreeConstants.PieceHeight;
        }
    }
}