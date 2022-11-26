using System;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.Tweens;
using Woodman.Locations;
using Woodman.Player.Movement.View;

namespace Woodman.Tutorial.Meta.Joystick
{
    [EcsSystem(typeof(MetaTutorialModule))]
    public class TutorialJoystickSystem : IInitSystem, IDestroySystem
    {
        private DataWorld _world;
        private EcsOneData<MetaTutorialData> _tutorialData;
        private EcsOneData<LocationData> _locationData;
        private MovementView _movementView;
        private TutorialCanvasView _tutorialCanvas;
        private TutorialJoystickView _joystickView;

        public void Init()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;

            _tutorialCanvas.ToggleMoveFinger(true);
            _movementView.CircleMovement.Toggle(false);
            _movementView.Reader.OnOnChangeMoveState += OnMove;
            ToVertical(-_joystickView.delta, ToRightTop);
        }

        private void OnMove(bool _)
        {
            _movementView.CircleMovement.Toggle(true);
            _tutorialCanvas.ToggleMoveFinger(false);
            ref var td = ref _tutorialData.GetData();
            td.firstStepComplete = true;
            td.isDirty = true;
            _locationData.GetData().tutorialArrowsProvider.logArrow.Show();
            _movementView.Reader.OnOnChangeMoveState -= OnMove;
        }

        private void ToBottom()
        {
            if (_tutorialData.GetData().firstStepComplete)
                return;
            ToVertical(-_joystickView.delta, ToRightTop);
        }

        private void ToRightTop()
        {
            var startPos = _joystickView.GetPos();
            var endPos = startPos + Vector3.one * _joystickView.delta;
            var tween = new TweenData
            {
                remain = _joystickView.time,
                update = r =>
                {
                    var normalized = (_joystickView.time - r) / _joystickView.time;
                    var f = _joystickView.easing.Evaluate(normalized);
                    var pos = Vector3.Lerp(startPos, endPos, f);
                    _joystickView.SetPosition(pos);
                },
                validate = () => _joystickView != null && _joystickView.gameObject.activeSelf,
                onEnd = () =>
                {
                    _joystickView.SetPosition(endPos);
                    ToVertical(-_joystickView.delta, ToLeftTop);
                }
            };
            _world.NewEntity().AddComponent(tween);
        }
        
        private void ToLeftTop()
        {
            var startPos = _joystickView.GetPos();
            var top = Vector3.up * _joystickView.delta;
            var left = Vector3.left * _joystickView.delta;
            var endPos = startPos + top + left;
            var tween = new TweenData
            {
                remain = _joystickView.time,
                update = r =>
                {
                    var normalized = (_joystickView.time - r) / _joystickView.time;
                    var f = _joystickView.easing.Evaluate(normalized);
                    var pos = Vector3.Lerp(startPos, endPos, f);
                    _joystickView.SetPosition(pos);
                },
                validate = () => _joystickView != null && _joystickView.gameObject.activeSelf,
                onEnd = () =>
                {
                    _joystickView.SetPosition(endPos);
                    ToVertical(-_joystickView.delta, ToRightTop);
                }
            };
            _world.NewEntity().AddComponent(tween);
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

        public void Destroy()
        {
            _movementView.Reader.OnOnChangeMoveState -= OnMove;
        }
    }
}