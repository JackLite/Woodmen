using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Meta;
using Woodman.Utils;

namespace Woodman.Player.Movement.View
{
    [EcsSystem(typeof(MetaModule))]
    public class ControlMovementSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private MetaUiProvider _metaUiProvider;
        private MetaViewProvider _metaViewProvider;
        private UiProvider _uiProvider;
        private EcsOneData<PlayerMovementData> _movementData;
        public void Init()
        {
            _metaUiProvider.MovementView.Reader.OnOnChangeMoveState += OnChangeMoveState;
        }

        private void OnChangeMoveState(bool isActive)
        {
            var reader = _metaUiProvider.MovementView.Reader;
            var circle = _metaUiProvider.MovementView.CircleMovement;
            ref var moveData = ref _movementData.GetData();
            if (isActive)
            {
                var pos = _uiProvider.MainCanvas.ScreenToCanvasPosition(reader.CurrentPointerPos);
                circle.SetStartPosition(pos);
            }
            else
            {
                moveData.input = Vector2.zero;
                circle.ResetToDefault();
            }

            moveData.isMove = isActive;
        }

        public void Run()
        {
            ref var moveData = ref _movementData.GetData();

            if (!moveData.isMove)
                return;

            var circleMovement = _metaUiProvider.MovementView.CircleMovement;
            var reader = _metaUiProvider.MovementView.Reader;
            moveData.input = circleMovement.Delta;
            if (moveData.input.magnitude > 1)
                moveData.input = moveData.input.normalized;
            var pos = _uiProvider.MainCanvas.ScreenToCanvasPosition(reader.CurrentPointerPos);

            circleMovement.SetPosition(pos);
        }

        public void Destroy()
        {
            _metaUiProvider.MovementView.Reader.OnOnChangeMoveState -= OnChangeMoveState;
        }
    }
}