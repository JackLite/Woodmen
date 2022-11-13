using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;

namespace Woodman.Felling
{
    /// <summary>
    /// Init fellings logic after switch scenes
    /// </summary>
    [EcsSystem(typeof(CoreModule))]
    public class FellingInitSystem : IPreInitSystem, IRunSystem
    {
        private EcsOneData<TreeModel> _currentTree;
        private FellingPositions _positions;
        private FellingCharacterController _fellingCharacter;
        private FellingViewProvider _viewProvider;
        private TreeGenerator _treeGenerator;
        private TreePiecesRepository _treePiecesRepository;

        public void PreInit()
        {
            var t = _currentTree.GetData();
            _treeGenerator.Generate(_positions.RootPos, t.size, _viewProvider.Environment);
            _fellingCharacter.InitFelling(_positions);
        }

        public void Run()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.G))
            {
                _treePiecesRepository.Destroy();
                var t = _currentTree.GetData();
                _treeGenerator.Generate(_positions.RootPos, t.size, _viewProvider.Environment);
            }
            #endif
        }
    }
}