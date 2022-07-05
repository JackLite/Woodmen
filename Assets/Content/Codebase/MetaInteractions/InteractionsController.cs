using MetaTrees;
using UnityEngine;
using Zenject;

namespace MetaInteractions
{
    /// <summary>
    /// Связывает обработчиков взаимодействия и события взаимодействия
    /// </summary>
    public class InteractionsController : IInitializable
    {
        private readonly TreeInteraction _treeInteraction;
        public InteractionsController(TreeInteraction treeInteraction)
        {
            _treeInteraction = treeInteraction;
        }
        
        public void Initialize()
        {
            var allInteracts = Object.FindObjectsOfType<InteractTarget>();
            foreach (var target in allInteracts)
            {
                if (target.InteractType == InteractTypeEnum.Tree)
                    target.OnStartInteract += _treeInteraction.OnInteract;
            }
        }
    }
}