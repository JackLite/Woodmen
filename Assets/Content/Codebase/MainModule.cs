using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulesFrameworkUnity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Woodman.Cheats;
using Woodman.Common;
using Woodman.Common.CameraProcessing;
using Woodman.Felling;
using Woodman.Felling.Settings;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;
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
        protected override async Task Setup()
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

            var rawFellingSettings = await Addressables.LoadAssetAsync<TextAsset>("FellingSettings").Task;
            var fellingSettings = JsonConvert.DeserializeObject<FellingSettings>(rawFellingSettings.text);
            CreateOneData(fellingSettings);

            var rawTreeGenerationSettings = await Addressables.LoadAssetAsync<TextAsset>("TreeGenerationSettings").Task;
            var treeGenerationSettings =
                JsonConvert.DeserializeObject<TreeGenerationSettings>(rawTreeGenerationSettings.text);
            CreateTreeGenerator(treeGenerationSettings);

            CreateOneData<TreeModel>();
            CreateOneData<PlayerMovementData>();

            EcsWorldContainer.World.InitModule<FellingModule, MainModule>();
            if (Debug.isDebugBuild)
            {
                EcsWorldContainer.World.InitModule<CheatsModule, MainModule>();
                EcsWorldContainer.World.ActivateModule<CheatsModule>();
            }

            EcsWorldContainer.World.ActivateModule<MainModule>();
        }

        private void CreateTreeGenerator(TreeGenerationSettings generationSettings)
        {
            var treeContainer = Object.FindObjectOfType<TreeContainer>();
            var treePieceBuilder = new TreePieceBuilder(treeContainer);
            var treePiecesRepository = new TreePiecesRepository();
            var treeGenerator = new TreeGenerator(
                treePieceBuilder,
                treePiecesRepository,
                EcsWorldContainer.World,
                generationSettings
            );
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