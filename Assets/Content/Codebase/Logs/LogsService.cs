using Woodman.MetaTrees;

namespace Woodman.Logs
{
    public class LogsService
    {
        private const int smallLogsHeap = 50;
        private const int logsHeap = 100;
        private const int bigLogsHeap = 200;

        public void ShowLogsAfterFelling(TreeMeta treeMeta)
        {
            //todo: добавить 4 размер кучи
            var treeModel = treeMeta.GetTreeModel();
            var remain = treeModel.logsCount;
            if (treeModel.logsCount > bigLogsHeap)
            {
                remain = treeModel.logsCount % bigLogsHeap;
                treeMeta.ShowLogs(LogsHeapType.Big, treeModel.logsCount - remain);
            }
            if (remain <= smallLogsHeap)
                treeMeta.ShowLogs(LogsHeapType.Small, remain);
            else
                treeMeta.ShowLogs(LogsHeapType.Middle, remain);
        }
    }
}