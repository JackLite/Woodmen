using System.Globalization;
using TMPro;
using UnityEngine;

namespace Utilities
{
    public sealed class FpsCounter : MonoBehaviour
    {
        public TMP_Text text;

        private const float UpdateFrequency = 1f;
        private float _timeSinceLastUpdateFps;
        private int _updateCount;
        private static bool _isExist;

        private void Awake()
        {
        #if PRODUCTION
            DestroyImmediate(gameObject);
        #else
            if (_isExist || text == null)
            {
                DestroyImmediate(gameObject);

                return;
            }

            _timeSinceLastUpdateFps = UpdateFrequency;
            _isExist = true;
            DontDestroyOnLoad(gameObject);
        #endif
        }

        private void Update()
        {
            _timeSinceLastUpdateFps += Time.unscaledDeltaTime;
            _updateCount++;
            if (_timeSinceLastUpdateFps > UpdateFrequency)
            {
                var fps = (int) (_updateCount / _timeSinceLastUpdateFps);
                text.text = fps.ToString(CultureInfo.InvariantCulture);
                _timeSinceLastUpdateFps = 0;
                _updateCount = 0;
            }
        }
    }
}