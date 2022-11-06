using System;
using System.Threading.Tasks;
using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common.UI;
using Woodman.Felling.Tree;
using Woodman.Locations.Trees;
using Woodman.Meta;

namespace Woodman.Felling.Start
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
            SwitchUI();
        }

        private async void SwitchUI()
        {
            try
            {
                _switchCoreScreen.Show();
                await Task.Delay(TimeSpan.FromSeconds(_switchCoreScreen.animDuration));
                _metaUi.Hide();
                _world.InitModule<CoreModule>();
                _world.DestroyModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}