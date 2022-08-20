using UnityEngine;
using Woodman.Common;
using Woodman.Felling.Timer;
using Zenject;

namespace Woodman.Felling
{
    public class FellingController : IInitializable
    {
        private readonly FellingEventBus _fellingEventBus;
        private readonly FellingProcessor _fellingProcessor;
        private readonly FellingTimer _fellingTimer;
        private readonly FellingUiProcessor _fellingUiProcessor;
        private readonly FellingSettingsContainer _settingsContainer;
        private readonly WindowsUiProvider _uiProvider;

        public FellingController(
            FellingProcessor fellingProcessor,
            FellingUiProcessor fellingUiProcessor,
            FellingTimer fellingTimer,
            FellingSettingsContainer settingsContainer,
            WindowsUiProvider uiProvider,
            FellingEventBus fellingEventBus)
        {
            _fellingProcessor = fellingProcessor;
            _fellingUiProcessor = fellingUiProcessor;
            _fellingTimer = fellingTimer;
            _settingsContainer = settingsContainer;
            _uiProvider = uiProvider;
            _fellingEventBus = fellingEventBus;
        }

        public void Initialize()
        {
            _fellingUiProcessor.OnStart += StartGame;
            _fellingProcessor.OnWin += OnWin;
            _fellingProcessor.OnGameOver += OnGameOver;
            _fellingTimer.OnEnd += OnGameOver;
            _uiProvider.FellingWinWindow.OnOkBtnClick += () => _fellingEventBus.OnEscapeFelling?.Invoke(true);
        }

        private void StartGame()
        {
            _fellingTimer.Start(_settingsContainer.GetSettings().time);
        }

        private void OnWin()
        {
            _fellingTimer.Stop();
            _uiProvider.FellingWinWindow.Show();
        }

        private void OnGameOver()
        {
            Debug.Log("Loose!");
        }
    }
}