using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling;
using Woodman.Utils;

namespace Woodman.Tutorial.Core.Taps
{
    [EcsSystem(typeof(CoreTutorialModule))]
    public class CoreTutorialTapsSystem : IInitSystem, IDestroySystem
    {
        private CoreTutorialTapPositions _tapPositions;
        private DataWorld _world;
        private EcsOneData<CoreTutorialData> _tutorialData;
        private FellingUIProvider _fellingUIProvider;
        private TutorialCanvasView _tutorialCanvas;

        public void Init()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete)
            {
                _tutorialCanvas.tapHand.Toggle(false);
                return;
            }

            if (!td.rightTapComplete)
                ShowTapHand(_tapPositions.right);
            else if (!td.leftTapComplete)
                ShowTapHand(_tapPositions.left);

            _fellingUIProvider.TapController.OnTap += OnTap;
        }

        private void ShowTapHand(RectTransform target)
        {
            _tutorialCanvas.tapHand.SetPosition(target);
            _tutorialCanvas.tapHand.Toggle(true);
            _tutorialCanvas.Show();
        }

        private void OnTap(FellingSide side)
        {
            ref var td = ref _tutorialData.GetData();
            if (td.baseComplete) return;
            td.tapsCount++;
            if (td.tapsCount >= 4 && (!td.rightTapComplete || !td.leftTapComplete))
            {
                td.rightTapComplete = true;
                td.leftTapComplete = true;
                FinishTapsTutorial();
            }
            if (side == FellingSide.Right && !td.rightTapComplete)
            {
                td.rightTapComplete = true;
                if (td.leftTapComplete)
                {
                    FinishTapsTutorial();
                }
                else
                {
                    _tutorialCanvas.tapHand.SetPosition(_tapPositions.left);
                }
            }

            if (side == FellingSide.Left && !td.leftTapComplete)
            {
                td.leftTapComplete = true;
                if (td.rightTapComplete)
                {
                    FinishTapsTutorial();
                }
                else
                {
                    _tutorialCanvas.tapHand.SetPosition(_tapPositions.right);
                }
            }
        }

        private void FinishTapsTutorial()
        {
            _tutorialCanvas.tapHand.Toggle(false);
            _world.CreateEvent<TutorialCoreFinishTapEvent>();
        }

        public void Destroy()
        {
            _fellingUIProvider.TapController.OnTap -= OnTap;
        }
    }
}