using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Utils;

namespace Woodman.Player.Movement.View
{
    [EcsSystem(typeof(MetaModule))]
    public class ControlMovementSystem : IInitSystem, IRunSystem, IDestroySystem
    {
        private MetaUiProvider _metaUiProvider;
        private MainViewProvider _mainViewProvider;
        private EcsOneData<PlayerMovementData> _movementData;
        public void Init()
        {
            _metaUiProvider.MovementViewProvider.Reader.onChangeMoveState += OnChangeMoveState;
        }

        private void OnChangeMoveState(bool isActive)
        {
            var reader = _metaUiProvider.MovementViewProvider.Reader;
            var circle = _metaUiProvider.MovementViewProvider.CircleMovement;
            ref var moveData = ref _movementData.GetData();
            if (isActive)
            {
                var pos = _mainViewProvider.MainCanvas.ScreenToCanvasPosition(reader.CurrentPointerPos);
                circle.SetStartPosition(pos);
            }
            else
            {
                moveData.input = Vector2.zero;
                circle.ResetToDefault();
                _mainViewProvider.PlayerMovement.StopMove();
            }

            moveData.isMove = isActive;
        }

        public void Run()
        {
            ref var moveData = ref _movementData.GetData();

            if (!moveData.isMove)
                return;

            var circleMovement = _metaUiProvider.MovementViewProvider.CircleMovement;
            var reader = _metaUiProvider.MovementViewProvider.Reader;
            moveData.input = circleMovement.Delta;
            var pos = _mainViewProvider.MainCanvas.ScreenToCanvasPosition(reader.CurrentPointerPos);

            circleMovement.SetPosition(pos);
        }

        public void Destroy()
        {
            _metaUiProvider.MovementViewProvider.Reader.onChangeMoveState -= OnChangeMoveState;
        }
    }
}