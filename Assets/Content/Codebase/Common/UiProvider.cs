using UnityEngine;
using Woodman.Common.UI;
using Woodman.Felling;
using Woodman.Felling.Lose;
using Woodman.Felling.Pause;
using Woodman.Felling.SecondChance;
using Woodman.Felling.Win;
using Woodman.Meta;
using Woodman.Settings;
using Woodman.Utils;

namespace Woodman.Common
{
    public class UiProvider : ViewProvider
    {
        [field: SerializeField]
        public Canvas MainCanvas { get; private set; }
        
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
        
        [field: SerializeField]
        [field: ViewInject]
        public PauseView PauseView { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public SettingsWindow SettingsView { get; private set; }
        
        [field: SerializeField]
        [field: ViewInject]
        public SecondChanceView SecondChanceView { get; private set; }

        private static UiProvider _inst;
        private void Awake()
        {
            if (_inst != null)
            {
                DestroyImmediate(gameObject);
                return;
            }

            _inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}