﻿using UnityEngine;
using Woodman.Cheats.View;
using Woodman.Common;
using Woodman.Loading;
using Woodman.Logs;
using Woodman.Logs.LogsUsing;
using Woodman.Player;
using Woodman.Player.Indicators;
using Woodman.Player.Movement.View;
using Woodman.Utils;

namespace Woodman.Meta
{
    /// <summary>
    ///     Инкапсулирует ссылки на монобехи на локациях
    /// </summary>
    public class MetaViewProvider : MonoBehaviour
    {
        [field: Header("Locations")]
        [field: SerializeField]
        [field: ViewInject]
        public LocationsView LocationsView { get; private set; }

        [field: Header("Player")]
        [field: SerializeField]
        [field: ViewInject]
        public PlayerMovement PlayerMovement { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public PlayerVisibilityDetector PlayerVisibilityDetector { get; private set; }

        [field: SerializeField]
        public GameObject WoodmanContainer { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public PlayerIndicatorsController PlayerIndicatorsController { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public CharacterLogsView CharacterLogsView { get; private set; }

        [field: Header("Pools")]
        [field: SerializeField]
        [field: ViewInject]
        public LogsPool LogsPool { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public PoolsProvider PoolsProvider { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public DebugViewProvider DebugViewProvider { get; private set; }
    }
}