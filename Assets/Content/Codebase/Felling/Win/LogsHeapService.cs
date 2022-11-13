using System.Collections.Generic;
using Woodman.Felling.Tree;
using Woodman.Logs;

namespace Woodman.Felling.Win
{
    public class LogsHeapService
    {
        private LogsHeapRepository _logsHeapRepository;
        
        //todo: move to settings
        private const int smallLogsHeap = 50;
        private const int logsHeap = 100;
        private const int bigLogsHeap = 200;

        public LogsHeapService(LogsHeapRepository logsHeapRepository)
        {
            _logsHeapRepository = logsHeapRepository;
        }

        public void SaveLogs(ref TreeModel treeModel)
        {
            //todo: добавить 4 размер кучи
            treeModel.createdHeaps = new List<string>();
            var remain = treeModel.size;
            var id = "";
            if (treeModel.size > bigLogsHeap)
            {
                remain = treeModel.size % bigLogsHeap;
                id = _logsHeapRepository.Create(LogsHeapType.Big, remain, treeModel.logsPositions[LogsHeapType.Big]);
                treeModel.createdHeaps.Add(id);
            }

            var heapType = remain <= smallLogsHeap ? LogsHeapType.Small : LogsHeapType.Middle;
            id = _logsHeapRepository.Create(heapType, remain, treeModel.logsPositions[heapType]);
            treeModel.createdHeaps.Add(id);
        }
    }
}