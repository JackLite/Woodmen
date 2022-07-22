using System;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingProcessor : IInitializable
    {
        private readonly TapController _tapController;
        private readonly FellingUIProvider _uiProvider;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly FellingCharacterController _characterController;
        private readonly TreeProgressService _progressService;

        public event Action OnGameOver;

        public FellingProcessor(
            FellingUIProvider uiProvider,
            TreePiecesRepository treePiecesRepository,
            FellingCharacterController characterController,
            TreeProgressService progressService)
        {
            _uiProvider = uiProvider;
            _treePiecesRepository = treePiecesRepository;
            _characterController = characterController;
            _progressService = progressService;
        }
        public void Initialize()
        {
            _uiProvider.TapController.OnTap += Cut;
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
            _progressService.UpdateAfterCut();
        }

        private bool CheckGameOver()
        {
            var piece = _treePiecesRepository.GetBottomPiece();
            return piece.IsHasBench && piece.Side == _characterController.CurrentSide;
        }
    }
}