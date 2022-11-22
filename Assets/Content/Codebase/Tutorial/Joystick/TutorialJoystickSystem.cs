using System;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.Tweens;
using Woodman.Locations;
using Woodman.Player.Movement.View;

namespace Woodman.Tutorial.Joystick
{
    [EcsSystem(typeof(TutorialModule))]
    public class TutorialJoystickSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<TutorialData> _tutorialData;
        private EcsOneData<LocationData> _locationData;
        private MovementView _movementView;
        private TutorialCanvasView _tutorialCanvas;
        private TutorialJoystickView _joystickView;

        public void Init()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;

            _tutorialCanvas.ToggleFinger(true);
            _movementView.CircleMovement.Toggle(false);
            _movementView.Reader.OnOnChangeMoveState += OnMove;
            ToRight();
        }

        private void OnMove(bool _)
        {
            _movementView.CircleMovement.Toggle(true);
            _tutorialCanvas.ToggleFinger(false);
            ref var td = ref _tutorialData.GetData();
            td.firstStepComplete = true;
            td.isDirty = true;
            _locationData.GetData().tutorialArrowsProvider.logArrow.Show();
            _movementView.Reader.OnOnChangeMoveState -= OnMove;
        }

        private void ToRight()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;
            ToHorizontal(_joystickView.delta, ToBottom);
        }

        private void ToLeft()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;
            ToHorizontal(-_joystickView.delta, ToTop);
        }

        private void ToBottom()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;
            ToVertical(-_joystickView.delta, ToLeft);
        }


        private void ToTop()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;
            ToVertical(_joystickView.delta, ToRight);
        }

        private void ToVertical(float delta, Action onEnd)
        {
            var startPos = _joystickView.GetPos();
            var endPos = startPos.y + delta;
            var tween = new TweenData
            {
                remain = _joystickView.time,
                update = r =>
                {
                    var normalized = (_joystickView.time - r) / _joystickView.time;
                    var posY = startPos.y + _joystickView.easing.Evaluate(normalized) * delta;
                    _joystickView.SetPosition(new Vector3(startPos.x, posY));
                },
                validate = () => _joystickView != null && _joystickView.gameObject.activeSelf,
                onEnd = () =>
                {
                    _joystickView.SetPosition(new Vector3(startPos.x, endPos));
                    onEnd?.Invoke();
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        private void ToHorizontal(float delta, Action onEnd)
        {
            var startPos = _joystickView.GetPos();
            var endPos = startPos.x + delta;
            var tween = new TweenData
            {
                remain = _joystickView.time,
                update = r =>
                {
                    var normalized = (_joystickView.time - r) / _joystickView.time;
                    var posX = startPos.x + _joystickView.easing.Evaluate(normalized) * delta;
                    _joystickView.SetPosition(new Vector3(posX, startPos.y));
                },
                validate = () => _joystickView != null && _joystickView.gameObject.activeSelf,
                onEnd = () =>
                {
                    _joystickView.SetPosition(new Vector3(endPos, startPos.y));
                    onEnd?.Invoke();
                }
            };
            _world.NewEntity().AddComponent(tween);
        }

        public void Destroy()
        {
            _movementView.Reader.OnOnChangeMoveState -= OnMove;
        }
    }
}