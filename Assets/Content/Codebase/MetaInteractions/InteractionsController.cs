using Woodman.MetaTrees;
using Zenject;

namespace Woodman.MetaInteractions
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
                {
                    target.OnStartInteract += _treeInteraction.OnStartInteract;
                    target.OnInteract += _treeInteraction.OnInteract;
                    target.OnEndInteract += _treeInteraction.OnEndInteract;
                }
            }
        }
    }
}