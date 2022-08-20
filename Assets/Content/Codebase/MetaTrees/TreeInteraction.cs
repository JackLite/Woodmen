using Woodman.Felling;
using Woodman.MetaInteractions;
using Woodman.Player.Indicators;

namespace Woodman.MetaTrees
{
    public class TreeInteraction
    {
        private readonly FellingInitializer _fellingInitializer;
        private readonly PlayerIndicatorsController _playerIndicators;
        private readonly MetaTreesRepository _treesRepository;

        public TreeInteraction(PlayerIndicatorsController playerIndicators, FellingInitializer fellingInitializer,
            MetaTreesRepository treesRepository)
        {
            _playerIndicators = playerIndicators;
            _fellingInitializer = fellingInitializer;
            _treesRepository = treesRepository;
        }

        public void OnStartInteract(InteractTarget target)
        {
            _playerIndicators.ShowHideTreeIndicator(true);
        }

        public void OnEndInteract(InteractTarget target)
        {
            _playerIndicators.ShowHideTreeIndicator(false);
        }

        public void OnInteract(InteractTarget target)
        {
            var treeInteract = target as TreeInteract;
            if (!treeInteract)
                return;

            _fellingInitializer.Init(treeInteract.TreeMeta);
            _treesRepository.CurrentTree = treeInteract.TreeMeta;
        }
    }
}