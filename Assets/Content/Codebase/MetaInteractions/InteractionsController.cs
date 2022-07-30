using Woodman.Buildings;
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
        private readonly BuildingInteraction _buildingInteraction;

        public InteractionsController(TreeInteraction treeInteraction, BuildingInteraction buildingInteraction)
        {
            _treeInteraction = treeInteraction;
            _buildingInteraction = buildingInteraction;
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
                else if (target.InteractType == InteractTypeEnum.Building)
                {
                    target.OnInteract += _buildingInteraction.OnInteract;
                }
            }
        }
    }
}