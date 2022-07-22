using Newtonsoft.Json;
using UnityEngine;

namespace Woodman.Felling
{
    /// <summary>
    /// Предоставляет доступ к настройкам кор-геймплея
    /// </summary>
    public class FellingSettingsContainer
    {
        private FellingSettings _settings;
        
        public FellingSettingsContainer(TextAsset rawSettings)
        {
            _settings = JsonConvert.DeserializeObject<FellingSettings>(rawSettings.text);
        }
        
        public FellingSettings GetSettings()
        {
            return _settings;
        }
    }
}