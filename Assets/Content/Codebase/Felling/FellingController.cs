using UnityEngine;
using Woodman.Felling.Timer;
using Woodman.MetaTrees;
using Zenject;

namespace Woodman.Felling
{
    public class FellingController : IInitializable
    {
        private readonly FellingProcessor _fellingProcessor;
        private readonly FellingUI _fellingUI;
        private readonly FellingTimer _fellingTimer;

        public FellingController(FellingProcessor fellingProcessor, FellingUI fellingUI, FellingTimer fellingTimer)
        {
            _fellingProcessor = fellingProcessor;
            _fellingUI = fellingUI;
            _fellingTimer = fellingTimer;
        }

        public void Initialize()
        {
            _fellingUI.OnStart += StartGame;
            _fellingProcessor.OnGameOver += OnGameOver;
            _fellingTimer.OnEnd += OnGameOver;
        }

        public void InitFelling(TreeMeta treeMeta)
        {
            _fellingUI.InitFelling();
        }

        private void StartGame()
        {
        }

        private void OnGameOver()
        {
            Debug.Log("Loose!");
        }
    }
}