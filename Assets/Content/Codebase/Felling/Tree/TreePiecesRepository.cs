using System.Collections.Generic;
using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiecesRepository
    {
        private readonly Queue<TreePiece> _pieces = new();
        private GameObject _parent;
        private TreePiece _finish;

        public void AddPiece(TreePiece piece)
        {
            _parent = piece.transform.parent.gameObject;
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
            foreach (var piece in _pieces)
            {
                yield return piece;
            }

            yield return _finish;
        }

        public TreePiece GetPiece(int offset)
        {
            var i = offset;
            foreach (var piece in _pieces)
            {
                --i;
                if (i < 0)
                    return piece;
            }

            return null;
        }

        public void Destroy()
        {
            foreach (var piece in _pieces)
            {
                Object.Destroy(piece.gameObject);
            }
            
            Object.Destroy(_finish.gameObject);
            _pieces.Clear();
            if (_parent != null) 
                Object.Destroy(_parent);
        }

        public void AddFinish(TreePiece finish)
        {
            _finish = finish;
        }
    }
}