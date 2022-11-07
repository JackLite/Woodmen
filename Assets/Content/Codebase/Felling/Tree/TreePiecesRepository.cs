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
        }

        public IEnumerable<TreePiece> GetPieces()
        {
            return _pieces;
        }

        public void Destroy()
        {
            foreach (var piece in _pieces)
            {
                Object.Destroy(piece.gameObject);
            }

            _pieces.Clear();
        }
    }
}