using UnityEngine;
using Woodman.Cheats;
using Woodman.Cheats.View;
using Woodman.Common.CameraProcessing;
using Woodman.Logs;
using Woodman.Player.Indicators;
using Woodman.Player.Movement.View;
using Woodman.Utils;

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

        [field: SerializeField]
        public Canvas MainCanvas { get; private set; }
        
        [field: Header("Player")]
        [field: SerializeField]
        [field: ViewInject]
        public PlayerMovement PlayerMovement { get; private set; }

        [field: SerializeField]
        public GameObject WoodmanContainer { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public PlayerIndicatorsController PlayerIndicatorsController { get; private set; }

        [field: Header("Pools")]
        [field: SerializeField]
        [field: ViewInject]
        public LogsPool LogsPool { get; private set; }

        [field: Header("UI")]
        [field: SerializeField]
        [field: ViewInject]
        public WindowsUiProvider WindowsUiProvider { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public MetaUiProvider MetaUiProvider { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public DebugViewProvider DebugViewProvider { get; private set; }
        
        [field:SerializeField]
        [field: ViewInject]
        public PoolsProvider PoolsProvider { get; private set; }
    }
}