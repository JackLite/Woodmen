using UnityEngine;
using Woodman.MetaTrees;
using Zenject;

namespace Woodman.Felling
{
    public class FellingController : IInitializable
    {
        private readonly FellingProcessor _fellingProcessor;
        private readonly FellingUI _fellingUI;

        public FellingController(FellingProcessor fellingProcessor, FellingUI fellingUI)
        {
            _fellingProcessor = fellingProcessor;
            _fellingUI = fellingUI;
        }

        public void Initialize()
        {
            _fellingUI.OnStart += StartGame;
            _fellingProcessor.OnGameOver += OnGameOver;
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