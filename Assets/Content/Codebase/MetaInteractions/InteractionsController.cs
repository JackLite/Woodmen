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
            foreach (var target in InteractionStaticPool.Targets)
            {
                if (target.InteractType == InteractTypeEnum.Tree)
                    target.OnStartInteract += _treeInteraction.OnInteract;
            }
        }
    }
}