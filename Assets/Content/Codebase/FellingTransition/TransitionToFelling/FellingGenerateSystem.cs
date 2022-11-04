using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using Woodman.Felling;
using Woodman.Felling.Tree;
using Woodman.Felling.Tree.Generator;

namespace Woodman.FellingTransition.TransitionToFelling
{
    /// <summary>
    /// Generate tree and activate core module after it
    /// </summary>
    [EcsSystem(typeof(CoreModule))]
    public class FellingGenerateSystem : IInitSystem
    {
        private EcsOneData<TreeModel> _currentTree;
        private FellingPositions _positions;
        private TreeGenerator _treeGenerator;

        public void Init()
        {
            var t = _currentTree.GetData();
            _treeGenerator.Generate(_positions.RootPos, t.size);
        }
    }
}