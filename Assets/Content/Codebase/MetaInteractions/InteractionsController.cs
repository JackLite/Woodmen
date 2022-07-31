using Woodman.Buildings;
using Woodman.Logs;
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
        private readonly LogsInteraction _logsInteraction;

        public InteractionsController(TreeInteraction treeInteraction, BuildingInteraction buildingInteraction,
            LogsInteraction logsInteraction)
        {
            _treeInteraction = treeInteraction;
            _buildingInteraction = buildingInteraction;
            _logsInteraction = logsInteraction;
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
                else if (target.InteractType == InteractTypeEnum.Logs)
                {
                    target.OnInteract += _logsInteraction.OnInteract;
                }
            }
        }
    }
}