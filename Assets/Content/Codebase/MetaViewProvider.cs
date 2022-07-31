using UnityEngine;
using Woodman.CameraProcessing;
using Woodman.Misc;
using Woodman.Player.Indicators;
using Woodman.Player.Movement;
using Woodman.Player.Movement.View;

namespace Woodman
{
    /// <summary>
    /// Инкапсулирует ссылки на монобехи
    /// </summary>
    public class MetaViewProvider : MonoBehaviour
    {
        [field:Header("Global")]
        [field:SerializeField]
        [field:ViewInject]
        public CamerasContainer CamerasContainer { get; private set; }

        [field:SerializeField]
        [field:ViewInject]
        public WindowsSwitcher WindowsSwitcher { get; private set; }

        [field:Header("Player")]
        [field:SerializeField]
        [field:ViewInject]
        public PlayerMovement PlayerMovement { get; private set; }

        [field:SerializeField]
        [field:ViewInject]
        public ControlMovementPlayer ControlMovementPlayer { get; private set; }

        [field:SerializeField]
        [field:ViewInject]
        public PlayerIndicatorsController PlayerIndicatorsController { get; private set; }
        
        [field:Header("UI")]
        [field:SerializeField]
        [field:ViewInject]
        public MetaUiProvider MetaUiProvider { get; private set; }
    }
}