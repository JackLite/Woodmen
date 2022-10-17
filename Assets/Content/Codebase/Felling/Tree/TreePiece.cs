using System.Globalization;
using TMPro;
using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiece : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject UsualPiece { get; private set; }

        [field: SerializeField]
        public GameObject StrongPiece { get; private set; }

        [SerializeField]
        private TMP_Text _strongText;

        public FellingSide BranchSide { get; set; }
        public bool IsHasBranch { get; set; }
        public TreePieceType PieceType { get; set; } = TreePieceType.Usual;
        public int Size { get; private set; }


        public void SetType(TreePieceType type)
        {
            PieceType = type;
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
            if (PieceType != TreePieceType.Strong) return;
            SetType(TreePieceType.Usual);
        }

        private void UpdateStrongText()
        {
            _strongText.text = $"x{Size.ToString(CultureInfo.InvariantCulture)}";
        }

        private void UpdateByType()
        {
            UsualPiece.SetActive(PieceType == TreePieceType.Usual);
            StrongPiece.SetActive(PieceType == TreePieceType.Strong);
        }
    }
}