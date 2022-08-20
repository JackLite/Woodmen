using System;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingProcessor : IInitializable
    {
        private readonly FellingCharacterController _characterController;
        private readonly TreeProgressService _progressService;
        private readonly TapController _tapController;
        private readonly TreePiecesRepository _treePiecesRepository;
        private readonly FellingUIProvider _uiProvider;

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

        public event Action OnWin;
        public event Action OnGameOver;

        private void Cut(FellingSide fellingSide)
        {
            _characterController.SetSide(fellingSide);
            if (CheckGameOver())
            {
                OnGameOver?.Invoke();
                return;
            }

            _characterController.Cut();
            _treePiecesRepository.RemovePiece();
            if (_treePiecesRepository.GetRemain() == 0)
            {
                OnWin?.Invoke();
                return;
            }

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
            return piece.IsHasBranch && piece.FellingSide == _characterController.CurrentFellingSide;
        }
    }
}