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
using Woodman.Tutorial.Core.Taps;
using Woodman.Tutorial.Meta;
using Woodman.Utils;

namespace Woodman.Tutorial.Core.Bubbles
{
    [EcsSystem(typeof(CoreTutorialModule))]
    public class CoreTutorialBaseBubbleSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private EcsOneData<CoreTutorialData> _tutorialData;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<TimerData> _timerData;
        private DataWorld _world;
        private FellingUi _fellingUi;
        private TutorialCanvasView _tutorialCanvas;
        private TutorialSaveService _tutorialSaveService;
        private TutorialSettings _tutorialSettings;
        private TreePiecesRepository _piecesRepository;

        public void Init()
        {
            _tutorialCanvas.bubbleView.OnBubbleClick += OnBubbleClick;
        }

        public void Run()
        {
            if (_tutorialData.GetData().baseComplete)
                return;
            CheckTapTutorial();
            if (_world.IsEventRaised<CutEvent>())
                OnTap();
        }

        public void Destroy()
        {
            _tutorialCanvas.bubbleView.OnBubbleClick -= OnBubbleClick;
        }

        private void OnTap()
        {
            if (_tutorialData.GetData().progressComplete)
                return;
            var remain = _piecesRepository.GetRemain();
            var progress = (float)remain / _currentTree.GetData().size;

            if (progress < 0.3)
            {
                ToggleTimerFreeze(true);
                ShowUIBubble(_fellingUi.progress);
                ProcessBubbleInteractOnShow();
                _tutorialCanvas.Show();
                _tutorialCanvas.ShowProgressBubble();
            }
        }

        private void CheckTapTutorial()
        {
            var q = _world.Select<TutorialCoreFinishTapEvent>();
            if (!q.Any())
                return;

            q.DestroyAll();

            ShowTimerBubble();
        }

        private void ShowTimerBubble()
        {
            ShowUIBubble(_fellingUi.timer);
            _tutorialCanvas.ShowTimerBubble();
        }

        private void ShowUIBubble(GameObject target)
        {
            var bubbleView = _tutorialCanvas.bubbleView;
            var objectCopy = Object.Instantiate(target, target.transform.parent);
            objectCopy.transform.SetParent(bubbleView.transform, true);

            var timerRect = (RectTransform)objectCopy.transform;
            var timerWorldPos = timerRect.position;
            timerRect.anchorMin = timerRect.anchorMax = Vector2.zero;
            timerRect.position = timerWorldPos;
            bubbleView.SetBubbleSide(FellingSide.Left);
            bubbleView.SetBubbleAnchor(timerRect.anchoredPosition);

            bubbleView.OnBubbleClick += () =>
            {
                if (objectCopy != null)
                    Object.Destroy(objectCopy);
            };
        }

        private void ShowBranchAware()
        {
            var piece = GetFirstPieceWithBranch();
            piece.Branch.gameObject.SetLayer(LayerMask.NameToLayer(_tutorialSettings.tutorialObjectsLayer), true);
            UpdateBubblePos(piece);
            var tween = new TweenData
            {
                remain = 1f,
                update = _ => UpdateBubblePos(piece),
                validate = () => piece != null && _tutorialCanvas != null
            };
            _world.NewEntity().AddComponent(tween);

            _tutorialCanvas.InitRenderTexture();
            _tutorialCanvas.ShowBranchesBubble();
        }

        private TreePiece GetFirstPieceWithBranch()
        {
            foreach (var piece in _piecesRepository.GetPieces())
            {
                if (!piece.IsHasBranch)
                    continue;
                return piece;
            }

            return null;
        }

        private void UpdateBubblePos(TreePiece piece)
        {
            var branchWorldPos = piece.Branch.transform.position;
            var xOffset = _tutorialSettings.branchXOffset * (piece.BranchSide == FellingSide.Left ? -1 : 1);
            _tutorialCanvas.SetBubblePos(piece.BranchSide, branchWorldPos, xOffset);
        }

        private void OnBubbleClick()
        {
            ref var td = ref _tutorialData.GetData();
            if (!td.timerComplete)
            {
                td.timerComplete = true;
                ShowBranchAware();
                return;
            }

            if (!td.branchComplete)
            {
                td.branchComplete = true;
                var piece = GetFirstPieceWithBranch();
                piece.Branch.gameObject.SetLayer(piece.transform.parent.gameObject.layer, true);
                ToggleTimerFreeze(false);
                _tutorialCanvas.ReleaseRenderTexture();
                _tutorialCanvas.bubbleView.Hide();
                _tutorialCanvas.Hide();
                return;
            }

            if (!td.progressComplete)
            {
                td.progressComplete = true;
                td.baseComplete = true;
                _tutorialSaveService.Save(td);
                ToggleTimerFreeze(false);
                _tutorialCanvas.bubbleView.Hide();
                _tutorialCanvas.Hide();
                return;
            }
        }

        private void ProcessBubbleInteractOnShow()
        {
            _tutorialCanvas.bubbleView.ToggleInteract(false);
            DelayedFactory.Create(_world, 1f, () => _tutorialCanvas.bubbleView.ToggleInteract(true));
        }

        private void ToggleTimerFreeze(bool state)
        {
            ref var timer = ref _timerData.GetData();
            timer.isPaused = state;
        }
    }
}