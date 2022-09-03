using Core;
using EcsCore;
using UnityEngine;
using Woodman.EcsCodebase.Felling;
using Woodman.EcsCodebase.Felling.Timer;
using Woodman.EcsCodebase.Felling.Win;
using Woodman.Felling.Tree;

namespace Woodman.Felling
{
    [EcsSystem(typeof(FellingModule))]
    public class CutSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<FellingSettings> _fellingSettings;
        private EcsOneData<TimerData> _timerData;
        private FellingCharacterController _characterController;
        private FellingUIProvider _fellingUIProvider;
        private TreePiecesRepository _treePiecesRepository;

        public void Init()
        {
            _fellingUIProvider.TapController.OnTap += Cut;
        }

        public void Destroy()
        {
            _fellingUIProvider.TapController.OnTap -= Cut;
        }
        
        private void Cut(FellingSide fellingSide)
        {
            _characterController.SetSide(fellingSide);
            if (CheckGameOver())
            {
                _world.NewEntity().AddComponent(new BranchCollide());
                return;
            }
            #if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Space))
            {
                _treePiecesRepository.Destroy();
                UpdateProgressUI();
                _characterController.SetSide(FellingSide.Right);
                _world.CreateOneFrame().AddComponent(new WinEvent());
                return;
            }
            #endif

            _characterController.Cut();
            _treePiecesRepository.RemovePiece();
            UpdateProgressUI();
            if (_treePiecesRepository.GetRemain() == 0)
            {
                _characterController.SetSide(FellingSide.Right);
                _world.CreateOneFrame().AddComponent(new WinEvent());
                return;
            }

            if (CheckGameOver())
            {
                _world.NewEntity().AddComponent(new BranchCollide());
                return;
            }

            ref var td = ref _timerData.GetData();
            td.remain += _fellingSettings.GetData().timeForCut;
        }

        private void UpdateProgressUI()
        {
            var remain = _treePiecesRepository.GetRemain();
            _fellingUIProvider.TreeUIProgress.SetProgress((float) remain / _currentTree.GetData().size);
        }

        private bool CheckGameOver()
        {
            var piece = _treePiecesRepository.GetBottomPiece();
            return piece.IsHasBranch && piece.FellingSide == _characterController.CurrentFellingSide;
        }
    }
}