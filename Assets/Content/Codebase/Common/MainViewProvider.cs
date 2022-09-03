using UnityEngine;
using Woodman.Common.CameraProcessing;
using Woodman.EcsCodebase.Player.Movement.View;
using Woodman.EcsCodebase.Utils;
using Woodman.Player.Indicators;

namespace Woodman.Common
{
    /// <summary>
    ///     Инкапсулирует ссылки на монобехи
    /// </summary>
    public class MainViewProvider : MonoBehaviour
    {
        [field: Header("Global")]
        [field: SerializeField]
        [field: ViewInject]
        public CamerasContainer CamerasContainer { get; private set; }

        [field: Header("Player")]
        [field: SerializeField]
        [field: ViewInject]
        public PlayerMovement PlayerMovement { get; private set; }

        [field: SerializeField]
        public GameObject WoodmanContainer { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public ControlMovementPlayer ControlMovementPlayer { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public PlayerIndicatorsController PlayerIndicatorsController { get; private set; }

        [field: Header("UI")]
        [field: SerializeField]
        [field: ViewInject]
        public WindowsUiProvider WindowsUiProvider { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public MetaUiProvider MetaUiProvider { get; private set; }
    }
}