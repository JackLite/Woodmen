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
using Woodman.Player;
using Woodman.Player.Movement.View;
using Woodman.Progress;

namespace Woodman.Felling.Start
{
    [EcsSystem(typeof(MetaModule))]
    public class TransitionToFellingSystem : IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<TreeModel> _currentTree;
        private EcsOneData<PlayerData> _playerData;
        private MetaTreesRepository _metaTrees;
        private ProgressionService _progressionService;
        private MetaUi _metaUi;
        private PlayerMovement _playerMovement;
        private InnerLoadingScreen _innerLoadingScreen;

        public void Run()
        {
            var q = _world.Select<MoveToFelling>();
            if (!q.TrySelectFirst(out MoveToFelling c))
                return;

            _metaTrees.CurrentTree = c.treeMeta;
            var treeModel = c.treeMeta.GetTreeModel();
            var treeSize = _progressionService.GetSize();
            treeModel.size = treeSize.size;
            _currentTree.SetData(treeModel);
            ref var pd = ref _playerData.GetData();
            pd.metaPos = _playerMovement.CurrentPos;
            SwitchUI();
        }

        private async void SwitchUI()
        {
            try
            {
                _innerLoadingScreen.Show();
                await Task.Delay(TimeSpan.FromSeconds(_innerLoadingScreen.animDuration));
                _metaUi.Hide();
                _world.InitModule<CoreModule>(true);
                _world.DestroyModule<MetaModule>();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}