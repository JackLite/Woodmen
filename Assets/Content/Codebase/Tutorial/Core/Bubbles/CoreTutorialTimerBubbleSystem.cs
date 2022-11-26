using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling;
using Woodman.Felling.Timer;
using Woodman.Felling.Tree;
using Woodman.Tutorial.Core.Taps;
using Woodman.Tutorial.Meta;
using Woodman.Utils;

namespace Woodman.Tutorial.Core.Bubbles
{
    [EcsSystem(typeof(CoreTutorialModule))]
    public class CoreTutorialTimerBubbleSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private EcsOneData<CoreTutorialData> _tutorialData;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<TimerData> _timerData;
        private DataWorld _world;
        private FellingUi _fellingUi;
        private FellingUIProvider _fellingUIProvider;
        private TutorialCanvasView _tutorialCanvas;
        private TutorialSaveService _tutorialSaveService;
        private TutorialSettings _tutorialSettings;
        private TreePiecesRepository _piecesRepository;

        public void Init()
        {
            _tutorialCanvas.bubbleView.OnBubbleClick += OnBubbleClick;
            _fellingUIProvider.TapController.OnTap += OnTap;
        }

        public void Run()
        {
            CheckTapTutorial();
        }

        public void Destroy()
        {
            _tutorialCanvas.bubbleView.OnBubbleClick -= OnBubbleClick;
            _fellingUIProvider.TapController.OnTap -= OnTap;
        }

        private void OnTap(FellingSide side)
        {
            if (_tutorialData.GetData().progressComplete)
                return;
            var remain = _piecesRepository.GetRemain();
            var progress = (float)remain / _currentTree.GetData().size;

            if (progress < 0.3)
            {
                ToggleTimerFreeze(true);
                ShowUIBubble(_fellingUi.progress);

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
            foreach (var piece in _piecesRepository.GetPieces())
            {
                if (!piece.IsHasBranch)
                    continue;
                piece.Branch.SetLayer(LayerMask.NameToLayer(_tutorialSettings.tutorialObjectsLayer), true);
                var branchWorldPos = piece.Branch.transform.position;
                var xOffset = _tutorialSettings.branchXOffset * (piece.BranchSide == FellingSide.Left ? -1 : 1);
                _tutorialCanvas.SetBubblePos(piece.BranchSide, branchWorldPos, xOffset);
                break;
            }

            _tutorialCanvas.ShowBranchesBubble();
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
                ToggleTimerFreeze(false);
                _tutorialCanvas.bubbleView.Hide();
                return;
            }

            if (!td.progressComplete)
            {
                td.progressComplete = true;
                td.baseComplete = true;
                ToggleTimerFreeze(false);
                _tutorialCanvas.bubbleView.Hide();
                _tutorialSaveService.Save(td);
                return;
            }
        }

        private void ToggleTimerFreeze(bool state)
        {
            ref var timer = ref _timerData.GetData();
            timer.isFreeze = state;
        }
    }
}