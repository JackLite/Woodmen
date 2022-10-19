using System.Globalization;
using TMPro;
using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiece : MonoBehaviour
    {
        [SerializeField]
        private GameObject _usualPiece;

        [SerializeField]
        private GameObject _strongPiece;

        [SerializeField]
        private GameObject _hollowPiece;

        [SerializeField]
        private TMP_Text _strongText;

        public FellingSide BranchSide { get; set; }
        public bool IsHasBranch { get; set; }
        private TreePieceType _pieceType = TreePieceType.Usual;
        public int Size { get; private set; }


        public void SetType(TreePieceType type)
        {
            _pieceType = type;
            UpdateByType();
        }

        public void SetSize(int size)
        {
            Size = size;
            UpdateStrongText();
        }

        public void DecrementSize()
        {
            Size--;
            if (Size > 1)
            {
                UpdateStrongText();
                return;
            }
            if (_pieceType != TreePieceType.Strong) return;
            SetType(TreePieceType.Usual);
        }

        private void UpdateStrongText()
        {
            _strongText.text = $"x{Size.ToString(CultureInfo.InvariantCulture)}";
        }

        private void UpdateByType()
        {
            _usualPiece.SetActive(_pieceType == TreePieceType.Usual);
            _strongPiece.SetActive(_pieceType == TreePieceType.Strong);
            _hollowPiece.SetActive(_pieceType == TreePieceType.Hollow);
        }
    }
}