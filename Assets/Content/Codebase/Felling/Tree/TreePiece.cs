using UnityEngine;

namespace Woodman.Felling.Tree
{
    public class TreePiece : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject UsualPiece { get; private set; }

        [field: SerializeField]
        public GameObject HollowPiece { get; private set; }

        public FellingSide BranchSide { get; set; }
        public bool IsHasBranch { get; set; }
        public TreePieceType PieceType { get; set; } = TreePieceType.Usual;
        public int Size { get; set; }


        public void SetType(TreePieceType type)
        {
            PieceType = type;
            UpdateByType();
        }

        public void DecrementSize()
        {
            Size--;
            if (PieceType != TreePieceType.Hollow) return;
            SetType(TreePieceType.Usual);
        }

        private void UpdateByType()
        {
            UsualPiece.SetActive(PieceType == TreePieceType.Usual);
            HollowPiece.SetActive(PieceType == TreePieceType.Hollow);
        }
    }
}