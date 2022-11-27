using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Unity.Mathematics;
using UnityEngine;
using Woodman.Felling.Settings;
using Woodman.Felling.Taps.CutFx;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Win;

namespace Woodman.Felling.Taps
{
    [EcsSystem(typeof(CoreModule))]
    public class CutSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private CutFxPool _cutFxPool;
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<FellingSettings> _fellingSettings;
        private EcsOneData<TimerData> _timerData;
        private FellingCharacterController _characterController;
        private FellingUIProvider _fellingUIProvider;
        private TreePiecesRepository _treePiecesRepository;

        public void Init()
        {
            _fellingUIProvider.TapController.OnTap += OnTap;
        }

        public void Destroy()
        {
            _fellingUIProvider.TapController.OnTap -= OnTap;
        }

        public void Run()
        {
            var q = _world.Select<InnerCutEvent>();
            if (!q.TrySelectFirst(out InnerCutEvent ev))
                return;
            Cut(ev.side);
            q.DestroyAll();
        }

        private void OnTap(FellingSide side)
        {
            _world.NewEntity().AddComponent(new InnerCutEvent { side = side });
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
            var piece = _treePiecesRepository.GetBottomPiece();
            piece.DecrementSize();
            if (piece.Size <= 0)
            {
                _treePiecesRepository.RemovePiece();
                _world.CreateOneFrame().AddComponent(new CutEvent
                {
                    size = piece.TotalSize
                });
                UpdateProgressUI();
                if (_treePiecesRepository.GetRemain() == 0)
                {
                    _characterController.SetSide(FellingSide.Right);
                    _world.CreateOneFrame().AddComponent(new WinEvent());
                    return;
                }
            }

            if (CheckGameOver())
            {
                _world.NewEntity().AddComponent(new BranchCollide());
                return;
            }

            ref var td = ref _timerData.GetData();
            if (!td.isPaused)
            {
                td.remain += _fellingSettings.GetData().timeForCut;
                td.remain = math.min(td.remain, td.totalTime);
            }
        }

        private void UpdateProgressUI()
        {
            var remain = _treePiecesRepository.GetRemain();
            _fellingUIProvider.TreeUIProgress.SetProgress((float)remain / _currentTree.GetData().size);
        }

        private bool CheckGameOver()
        {
            var piece = _treePiecesRepository.GetBottomPiece();
            return piece.IsHasBranch && piece.BranchSide == _characterController.CurrentFellingSide;
        }
    }
}