using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;
using UnityEngine;
using Woodman.Common;
using Woodman.Logs;
using Woodman.MetaTrees;

namespace Woodman.FellingTransition.FellingEscape
{
    [EcsSystem(typeof(MetaModule))]
    public class FellingReturnWinSystem : IInitSystem, IDestroySystem
    {
        //todo: move to settings
        private const int smallLogsHeap = 50;
        private const int logsHeap = 100;
        private const int bigLogsHeap = 200;

        private LogsPool _logsPool;
        private LogsHeapRepository _logsHeapRepository;
        private MetaTreesRepository _metaTrees;
        private UiProvider _uiProvider;

        public void Init()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick += OnWinClick;
        }

        private void OnWinClick()
        {
            //todo: добавить 4 размер кучи
            var treeMeta = _metaTrees.CurrentTree;
            var treeModel = treeMeta.GetTreeModel();
            var remain = treeModel.logsCount;
            if (treeModel.logsCount > bigLogsHeap)
            {
                remain = treeModel.logsCount % bigLogsHeap;
                Show(LogsHeapType.Big, treeMeta.GetLogsPos(LogsHeapType.Big), treeModel.logsCount - remain);
            }

            var heapType = remain <= smallLogsHeap ? LogsHeapType.Small : LogsHeapType.Middle;
            Show(heapType, treeMeta.GetLogsPos(heapType), remain);
        }

        private void Show(LogsHeapType heapType, Vector3 pos, int count)
        {
            var id = _logsHeapRepository.Create(heapType, count, pos);
            var view = _logsPool.GetLogView(heapType);
            view.Id = id;
            view.SetCount(count);
            //todo: показывать с анимацией
            view.transform.position = pos;
            view.Show();
        }

        public void Destroy()
        {
            _uiProvider.FellingWinWindow.OnOkBtnClick -= OnWinClick;
        }
    }
}