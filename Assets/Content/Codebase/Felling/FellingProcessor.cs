using System;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingProcessor : IInitializable
    {
        private readonly TapController _tapController;
        private readonly FellingUIProvider _fellingUI;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly FellingCharacterController _characterController;
        private readonly FellingTimer _fellingTimer;

        public event Action OnGameOver;

        public FellingProcessor(
            FellingUIProvider fellingUI,
            TreePiecesRepository treePiecesRepository,
            FellingCharacterController characterController,
            FellingTimer fellingTimer)
        {
            _fellingUI = fellingUI;
            _treePiecesRepository = treePiecesRepository;
            _characterController = characterController;
            _fellingTimer = fellingTimer;
        }
        public void Initialize()
        {
            _fellingUI.TapController.OnTap += Cut;
        }

        private void Cut(Side side)
        {
            _characterController.SetSide(side);
            if (CheckGameOver())
            {
                OnGameOver?.Invoke();
                return;
            }
            _characterController.Cut();
            _treePiecesRepository.RemovePiece();
            if (CheckGameOver())
            {
                OnGameOver?.Invoke();
                return;
            }
            _fellingTimer.AddTime(0.1f);
        }

        private bool CheckGameOver()
        {
            var piece = _treePiecesRepository.GetBottomPiece();
            return piece.IsHasBench && piece.Side == _characterController.CurrentSide;
        }
    }
}