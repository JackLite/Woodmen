using UnityEngine;
using Woodman.Felling;
using Woodman.Felling.Win;
using Woodman.Meta;
using Woodman.Utils;

namespace Woodman.Common
{
    public class WindowsUiProvider : ViewProvider
    {
        [field: SerializeField]
        public FellingWinWindow FellingWinWindow { get; private set; }

        [field: SerializeField]
        public MetaUi MetaUi { get; private set; }
        
        [field: SerializeField]
        public FellingUi FellingUi { get; private set; }
    }
}