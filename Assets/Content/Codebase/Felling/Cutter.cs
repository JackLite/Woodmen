using UnityEngine;

namespace Woodman.Felling
{
    public class Cutter
    {
        private readonly PiecesController _piecesController;
        private readonly FellingPlayerController _playerController;
        public Cutter (PiecesController piecesController, FellingPlayerController playerController)
        {
            _piecesController = piecesController;
            _playerController = playerController;
        }

        public void Cut(Side side)
        {
            _playerController.MoveToSide(side);
            if (CheckGameOver())
            {
                Debug.Log("Loose!");
                return;
            }
            _piecesController.RemovePiece();
            if (CheckGameOver())
            {
                Debug.Log("Loose!");
            }
        }

        private bool CheckGameOver()
        {
            var piece = _piecesController.GetBottomPiece();
            return piece.IsHasBench && piece.Side == _playerController.CurrentSide;
        }
    }
}