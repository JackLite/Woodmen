using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulesFrameworkUnity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Woodman.Common.UI;
using Woodman.Felling;
using Woodman.Felling.Settings;
using Woodman.Felling.Taps;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Builder;
using Woodman.Felling.Tree.Generator;
using Woodman.Locations;
using Woodman.Progress;
using Woodman.Settings;
using Woodman.Utils;
using Object = UnityEngine.Object;

namespace Woodman
{
    public class CoreModule : EcsModuleWithDependencies
    {
        private SceneInstance _coreScene;

        protected override async Task Setup()
        {
            if (SceneManager.GetActiveScene().name != "CoreScene")
                _coreScene = await Addressables.LoadSceneAsync("CoreScene").Task;
            var rawTreeGenerationSettings = await Addressables.LoadAssetAsync<TextAsset>("TreeGenerationSettings").Task;
            var treeGenerationSettings =
                JsonConvert.DeserializeObject<TreeGenerationSettings>(rawTreeGenerationSettings.text);
            CreateTreeGenerator(treeGenerationSettings);

            CreateOneData<TreePiecesData>();

            var fellingViewProvider = Object.FindObjectOfType<FellingViewProvider>(true);
            var tasks = new[]
            {
                fellingViewProvider.CutFxPool.WarmUp(20),
                fellingViewProvider.CutTextPool.WarmUp(20)
            };
            await Task.WhenAll(tasks);
            AddDependency(fellingViewProvider);
            BindView(fellingViewProvider);
            EcsWorldContainer.World.InitModule<FellingModule>();
            EcsWorldContainer.World.ActivateModule<CoreModule>();
        }

        public override void OnActivate()
        {
            GetGlobalDependency<StartupModule, InnerLoadingScreen>().Hide();
        }

        public override void OnDestroy()
        {
            Addressables.Release(_coreScene);
        }

        protected override Dictionary<Type, int> GetSystemsOrder()
        {
            return new Dictionary<Type, int>
            {
                { typeof(CutSystem), -10 }
            };
        }

        private void CreateTreeGenerator(TreeGenerationSettings generationSettings)
        {
            var treeContainer = Object.FindObjectOfType<TreeContainer>();
            var visualSettings = GetGlobalDependency<StartupModule, VisualSettings>();
            var treePieceBuilder = new TreePieceBuilder(treeContainer, visualSettings);
            var treePiecesRepository = new TreePiecesRepository();
            var progressionService = GetGlobalDependency<StartupModule, ProgressionService>();
            var locationIndex = progressionService.GetLocationIndex();
            var treeGenerator = new TreeGenerator(
                treePieceBuilder,
                treePiecesRepository,
                EcsWorldContainer.World,
                generationSettings,
                locationIndex
            );
            AddDependency(treePiecesRepository);
            AddDependency(treeGenerator);
        }
    }
}