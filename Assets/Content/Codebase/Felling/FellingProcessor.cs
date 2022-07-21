using System;
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

        public event Action OnGameOver;
        
        public FellingProcessor (FellingUIProvider fellingUI, TreePiecesRepository treePiecesRepository, FellingCharacterController characterController)
        {
            _fellingUI = fellingUI;
            _treePiecesRepository = treePiecesRepository;
            _characterController = characterController;
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
            }
        }

        private bool CheckGameOver()
        {
            var piece = _treePiecesRepository.GetBottomPiece();
            return piece.IsHasBench && piece.Side == _characterController.CurrentSide;
        }
    }
}