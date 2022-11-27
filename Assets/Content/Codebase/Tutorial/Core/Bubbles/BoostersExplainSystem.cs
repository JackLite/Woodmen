using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.Delay;
using Woodman.Common.Tweens;
using Woodman.Felling;
using Woodman.Felling.Taps;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Branches;
using Woodman.Tutorial.Meta;
using Woodman.Utils;

namespace Woodman.Tutorial.Core.Bubbles
{
    [EcsSystem(typeof(CoreTutorialModule))]
    public class BoostersExplainSystem : IRunSystem
    {
        private EcsOneData<CoreTutorialData> _tutorialData;
        private EcsOneData<TimerData> _timerData;
        private DataWorld _world;
        private TreePiecesRepository _piecesRepository;
        private TutorialCanvasView _tutorialCanvas;
        private TutorialSettings _tutorialSettings;
        private TutorialSaveService _saveService;

        public void Run()
        {
            if (!_world.IsEventRaised<CutEvent>())
                return;
            var piece = _piecesRepository.GetPiece(5);
            if (piece == null)
                return;
            if (!piece.IsHasBranch)
                return;
            if (piece.Branch.IsHasBooster())
            {
                ProcessBoosters(piece);
            }
            else if (piece.Branch.IsHasHive())
            {
                ProcessHive(piece);
            }
        }

        private void ProcessHive(TreePiece piece)
        {
            ref var data = ref _tutorialData.GetData();
            if (data.hiveComplete)
                return;
            var branchElement = piece.Branch.GetActiveElement();
            InitBubble(piece, branchElement);
            StopTimer();
            ShowHive(branchElement);
        }
        
        private void ShowHive(BranchElementView branchElementView)
        {
            ProcessBubbleInteractOnShow();
            _tutorialCanvas.ShowHiveBubble();
            _tutorialCanvas.bubbleView.OnBubbleClick += () =>
            {
                ref var data = ref _tutorialData.GetData();
                if (data.hiveComplete)
                    return;
                data.hiveComplete = true;
                _saveService.Save(data);
                StartTimer();
                _tutorialCanvas.bubbleView.Hide();
                _tutorialCanvas.Hide();
                ResetLayer(branchElementView);
            };
        }

        private static void ResetLayer(BranchElementView branchElementView)
        {
            branchElementView.gameObject.SetLayer(branchElementView.transform.parent.gameObject.layer, true);
        }

        private void ProcessBoosters(TreePiece piece)
        {
            var boosterType = piece.Branch.GetBoosterType();
            if (boosterType == BoosterType.Undefined)
                return;
            ref var data = ref _tutorialData.GetData();
            if (data.freezeComplete && boosterType == BoosterType.TimeFreeze)
                return;
            if (data.refillComplete && boosterType == BoosterType.RestoreTime)
                return;
            var branchElement = piece.Branch.GetActiveElement();
            InitBubble(piece, branchElement);
            StopTimer();
            if (boosterType == BoosterType.TimeFreeze)
                ShowFreeze(branchElement);
            else
                ShowRestore(branchElement);
        }

        private void ShowRestore(BranchElementView branchElementView)
        {
            ProcessBubbleInteractOnShow();
            _tutorialCanvas.ShowRestoreBubble();
            _tutorialCanvas.bubbleView.OnBubbleClick += () =>
            {
                ref var data = ref _tutorialData.GetData();
                if (data.refillComplete)
                    return;
                data.refillComplete = true;
                _saveService.Save(data);
                StartTimer();
                _tutorialCanvas.bubbleView.Hide();
                _tutorialCanvas.Hide();
                ResetLayer(branchElementView);
            };
        }

        private void ShowFreeze(BranchElementView branchElementView)
        {
            ProcessBubbleInteractOnShow();
            _tutorialCanvas.ShowFreezeBubble();
            _tutorialCanvas.bubbleView.OnBubbleClick += () =>
            {
                ref var data = ref _tutorialData.GetData();
                if (data.freezeComplete)
                    return;
                data.freezeComplete = true;
                _saveService.Save(data);
                StartTimer();
                _tutorialCanvas.bubbleView.Hide();
                _tutorialCanvas.Hide();
                ResetLayer(branchElementView);
            };
        }

        private void StopTimer()
        {
            ref var timer = ref _timerData.GetData();
            timer.isPaused = true;
        }

        private void StartTimer()
        {
            ref var timer = ref _timerData.GetData();
            timer.isPaused = false;
        }

        private void InitBubble(TreePiece piece, BranchElementView branchElement)
        {
            branchElement.gameObject.SetLayer(LayerMask.NameToLayer(_tutorialSettings.tutorialObjectsLayer), true);
            UpdateBubblePos(piece.BranchSide, branchElement);
            var tween = new TweenData
            {
                remain = 1,
                update = _ => UpdateBubblePos(piece.BranchSide, branchElement),
                validate = () => branchElement != null && _tutorialCanvas != null
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void UpdateBubblePos(FellingSide side, BranchElementView elementView)
        {
            var branchWorldPos = elementView.transform.position;
            var yOffset = _tutorialCanvas.bubbleView.Height;
            _tutorialCanvas.SetBubblePos(side, branchWorldPos, 0, -yOffset);
        }
        
        private void ProcessBubbleInteractOnShow()
        {
            _tutorialCanvas.bubbleView.ToggleInteract(false);
            DelayedFactory.Create(_world, 1f, () => _tutorialCanvas.bubbleView.ToggleInteract(true));
        }
    }
}