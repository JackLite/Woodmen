using System;
using UnityEngine;
using Woodman.Common.UI;
using Woodman.Felling;
using Woodman.Felling.Lose;
using Woodman.Felling.Win;
using Woodman.Meta;
using Woodman.Utils;

namespace Woodman.Common
{
    public class UiProvider : ViewProvider
    {
        [field: SerializeField]
        [field: ViewInject]
        public MetaUiProvider MetaUiProvider { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public FellingWinWindow FellingWinWindow { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public FellingLoseWindow FellingLoseWindow { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public MetaUi MetaUi { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public FellingUi FellingUi { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public GameObject LoadScreen { get; private set; }

        [field: SerializeField]
        [field: ViewInject]
        public SwitchCoreScreen SwitchCoreScreen { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public FellingUIProvider FellingUIProvider { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}