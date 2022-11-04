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
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;

namespace Woodman
{
    public class CoreModule : EcsModuleWithDependencies
    {
        private SceneInstance _coreScene;

        protected override async Task Setup()
        {
            if (SceneManager.GetActiveScene().name != "CoreScene")
                _coreScene = await Addressables.LoadSceneAsync("CoreScene").Task;
            // внедрить зависимости связанные с корой
            var rawTreeGenerationSettings = await Addressables.LoadAssetAsync<TextAsset>("TreeGenerationSettings").Task;
            var treeGenerationSettings =
                JsonConvert.DeserializeObject<TreeGenerationSettings>(rawTreeGenerationSettings.text);
            CreateTreeGenerator(treeGenerationSettings);
            
            var fellingViewProvider = Object.FindObjectOfType<FellingViewProvider>();
            AddDependency(fellingViewProvider);
            BindView(fellingViewProvider);
            // сгенерировать дерево
            EcsWorldContainer.World.ActivateModule<CoreModule>();
        }

        public override void OnActivate()
        {
            GetGlobalDependency<StartupModule, SwitchCoreScreen>().Hide();
        }

        public override void OnDestroy()
        {
            Addressables.Release(_coreScene);
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
    }
}