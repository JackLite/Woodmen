using Woodman.Felling;
using Woodman.MetaInteractions;
using Woodman.Player.Indicators;

namespace Woodman.MetaTrees
{
    public class TreeInteraction
    {
        private readonly PlayerIndicatorsController _playerIndicators;
        private readonly FellingInitializer _fellingInitializer;
        public TreeInteraction(PlayerIndicatorsController playerIndicators, FellingInitializer fellingInitializer)
        {
            _playerIndicators = playerIndicators;
            _fellingInitializer = fellingInitializer;
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
        }
    }
}