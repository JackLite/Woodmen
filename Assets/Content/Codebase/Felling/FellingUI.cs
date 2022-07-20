using System;
using Zenject;

namespace Woodman.Felling
{
    public class FellingUI : IInitializable
    {
        private readonly FellingUIProvider _uiProvider;

        /// <summary>
        /// Вызывается когда игрок начинает кор-геймплей
        /// </summary>
        public event Action OnStart;

        public FellingUI(FellingUIProvider uiProvider)
        {
            _uiProvider = uiProvider;
        }

        public void Initialize()
        {
            _uiProvider.StartGameBtn.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            _uiProvider.StartGameBtn.gameObject.SetActive(false);
            OnStart?.Invoke();
        }
    }
}