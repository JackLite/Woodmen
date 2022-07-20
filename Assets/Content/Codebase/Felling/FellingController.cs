using UnityEngine;
using Woodman.Felling.Tree;
using Zenject;

namespace Woodman.Felling
{
    public class FellingController : IInitializable
    {
        private readonly TreeGenerator _treeGenerator;
        private readonly FellingProcessor _fellingProcessor;
        private readonly FellingUI _fellingUI;

        public FellingController(TreeGenerator treeGenerator, FellingProcessor fellingProcessor, FellingUI fellingUI)
        {
            _treeGenerator = treeGenerator;
            _fellingProcessor = fellingProcessor;
            _fellingUI = fellingUI;
        }

        public void Initialize()
        {
            _fellingUI.OnStart += StartGame;
            _fellingProcessor.OnGameOver += OnGameOver;
        }
        private void StartGame()
        {
            _treeGenerator.Generate();
        }
        private void OnGameOver()
        {
            Debug.Log("Loose!");
        }
    }
}