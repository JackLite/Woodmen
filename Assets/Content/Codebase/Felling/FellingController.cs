using UnityEngine;
using Woodman.Felling.Timer;
using Zenject;

namespace Woodman.Felling
{
    public class FellingController : IInitializable
    {
        private readonly FellingProcessor _fellingProcessor;
        private readonly FellingUI _fellingUI;
        private readonly FellingTimer _fellingTimer;
        private readonly FellingSettingsContainer _settingsContainer;

        public FellingController(
            FellingProcessor fellingProcessor,
            FellingUI fellingUI,
            FellingTimer fellingTimer,
            FellingSettingsContainer settingsContainer)
        {
            _fellingProcessor = fellingProcessor;
            _fellingUI = fellingUI;
            _fellingTimer = fellingTimer;
            _settingsContainer = settingsContainer;
        }

        public void Initialize()
        {
            _fellingUI.OnStart += StartGame;
            _fellingProcessor.OnWin += OnWin;
            _fellingProcessor.OnGameOver += OnGameOver;
            _fellingTimer.OnEnd += OnGameOver;
        }

        private void StartGame()
        {
            _fellingTimer.Start(_settingsContainer.GetSettings().time);
        }

        private void OnWin()
        {
            Debug.Log("Win!");
        }
        private void OnGameOver()
        {
            Debug.Log("Loose!");
        }
    }
}