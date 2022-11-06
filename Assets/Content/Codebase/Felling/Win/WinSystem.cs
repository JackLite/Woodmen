using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Common;
using Woodman.Felling.Tree;
using Woodman.Locations.Trees;
using Woodman.Logs;

namespace Woodman.Felling.Win
{
    [EcsSystem(typeof(FellingModule))]
    public class WinSystem : IPostRunSystem
    {
        private DataWorld _world;
        private MetaTreesRepository _treesRepository;
        private LogsHeapRepository _logsHeapRepository;
        private UiProvider _windows;
        private EcsOneData<TreeModel> _treeModel;
        
        //todo: move to settings
        private const int smallLogsHeap = 50;
        private const int logsHeap = 100;
        private const int bigLogsHeap = 200;
        public void PostRun()
        {
            var q = _world.Select<WinEvent>();
            if (!q.Any())
                return;
            
            _treesRepository.SetFell(_treesRepository.CurrentTree.Id);
            SaveLogs();
            _world.DeactivateModule<FellingModule>();
            _windows.FellingUi.Hide();
            _windows.FellingWinWindow.Show();
        }

        private void SaveLogs()
        {
            //todo: добавить 4 размер кучи
            var treeModel = _treeModel.GetData();
            var remain = treeModel.logsCount;
            if (treeModel.logsCount > bigLogsHeap)
            {
                remain = treeModel.logsCount % bigLogsHeap;
                _logsHeapRepository.Create(LogsHeapType.Big, remain, treeModel.logsPositions[LogsHeapType.Big]);
            }

            var heapType = remain <= smallLogsHeap ? LogsHeapType.Small : LogsHeapType.Middle;
            _logsHeapRepository.Create(heapType, remain, treeModel.logsPositions[heapType]);
        }
    }
}