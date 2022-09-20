using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Woodman.Common;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.FellingTransition;
using Woodman.MetaInteractions;
using Woodman.MetaInteractions.TreeInteraction;
using Woodman.Player.Movement.View;
using Woodman.PlayerRes;
using Object = UnityEngine.Object;

namespace Woodman
{
    public class MainModule : EcsModuleWithDependencies
    {
        protected override Task Setup()
        {
            var mainViewProvider = GetGlobalDependency<StartupModule, MainViewProvider>();

            var fellingViewProvider = Object.FindObjectOfType<FellingViewProvider>();
            AddDependency(new PlayerResRepository());
            AddDependency(mainViewProvider.WindowsUiProvider);
            AddDependency(mainViewProvider.PlayerIndicatorsController);
            AddDependency(mainViewProvider.MetaUiProvider.ResourceBarMetaUI);
            AddDependency(Object.FindObjectOfType<FellingCharacterController>());
            AddDependency(new CameraController(mainViewProvider.CamerasContainer));

            var fellingUiSwitcher =
                new FellingUiSwitcher(mainViewProvider.WindowsUiProvider, fellingViewProvider.FellingUIProvider);
            AddDependency(fellingUiSwitcher);
            AddDependency(fellingViewProvider);
            AddDependency(fellingViewProvider.FellingUIProvider);

            CreateTreeGenerator();
            
            CreateOneData<TreeModel>();
            CreateOneData<PlayerMovementData>();

            return Task.CompletedTask;
        }

        private void CreateTreeGenerator()
        {
            var treeContainer = Object.FindObjectOfType<TreeContainer>();
            var treePieceFactory = new TreePieceFactory(treeContainer);
            var treePiecesRepository = new TreePiecesRepository();
            var treeGenerator = new TreeGenerator(treePieceFactory, treePiecesRepository);
            AddDependency(treePiecesRepository);
            AddDependency(treeGenerator);
        }

        protected override Dictionary<Type, int> GetSystemsOrder()
        {
            return new Dictionary<Type, int>
            {
                { typeof(InteractionRegisterSystem), -100 },
                { typeof(TreeInteractionSystem), -10 }
            };
        }
    }
}