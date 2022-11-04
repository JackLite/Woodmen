using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common.UI;
using Woodman.Felling.Tree;
using Woodman.Meta;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.TransitionToFelling
{
    [EcsSystem(typeof(MetaModule))]
    public class TransitionToFellingSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private MetaTreesRepository _metaTrees;
        private MetaUi _metaUi;
        private SwitchCoreScreen _switchCoreScreen;

        public void Run()
        {
            var q = _world.Select<MoveToFelling>();
            if (!q.TrySelectFirst(out MoveToFelling c))
                return;

            _metaTrees.CurrentTree = c.treeMeta;
            var treeModel = c.treeMeta.GetTreeModel();
            _currentTree.SetData(treeModel);
            _switchCoreScreen.Show();
            _metaUi.Hide();
            _world.InitModule<CoreModule>();
            _world.DestroyModule<MetaModule>();
        }
    }
}