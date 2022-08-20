using Woodman.MetaTrees;

namespace Woodman.Logs
{
    public class LogsService
    {
        private const int smallLogsHeap = 50;
        private const int logsHeap = 99;
        private const int bigLogsHeap = 100;

        public void ShowLogsAfterFelling(TreeMeta treeMeta)
        {
            var treeModel = treeMeta.GetTreeModel();
            var bigLogsCount = treeModel.logsCount / bigLogsHeap;
            // todo: show many log's heaps
            treeMeta.ShowLogs(LogsHeapType.Big, bigLogsCount * bigLogsHeap);
            var remain = treeModel.logsCount % bigLogsHeap;
            if (remain <= smallLogsHeap)
                treeMeta.ShowLogs(LogsHeapType.Small, remain);
            else
                treeMeta.ShowLogs(LogsHeapType.Middle, remain);
        }
    }
}