using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Logs;

namespace Woodman.MetaTrees
{
    public class TreeMeta : MonoBehaviour
    {
        [SerializeField]
        private int _size = 100;

        [SerializeField]
        private Transform _leftCutPosition;

        [SerializeField]
        private Transform _rightCutPosition;

        [SerializeField]
        private GameObject _metaContent;

        [SerializeField]
        private int _logsCount;

        [SerializeField]
        private LogsTypeView[] _logsTypeViews;

        private Dictionary<LogsHeapType, LogView> _logsHeapTypeToViews = new();

        private void Awake()
        {
            _logsHeapTypeToViews = _logsTypeViews.ToDictionary(l => l.type, l => l.logView);
        }

        public void EnableMeta()
        {
            _metaContent.SetActive(true);
        }

        public void DisableMeta()
        {
            _metaContent.SetActive(false);
        }

        public TreeModel GetTreeModel()
        {
            return new TreeModel
            {
                pos = transform.position,
                leftPos = _leftCutPosition.position,
                rightPos = _rightCutPosition.position,
                size = _size,
                logsCount = _logsCount
            };
        }

        public void ShowLogs(LogsHeapType type, int resourceCount)
        {
            _logsHeapTypeToViews[type].SetCount(resourceCount);
            _logsHeapTypeToViews[type].Show();
        }

        [Serializable]
        private struct LogsTypeView
        {
            public LogsHeapType type;
            public LogView logView;
        }
    }
}