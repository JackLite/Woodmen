using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Logs;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Locations.Trees
{
    public class TreeMeta : MonoBehaviour
    {
        [ContextMenuItem("Generate guid", nameof(GenerateGuid))]
        [SerializeField]
        private string _guid;
        
        [SerializeField]
        private int _size = 100;

        [SerializeField]
        private GameObject _metaContent;

        [SerializeField]
        private int _logsCount;

        [SerializeField]
        private LogsTypeView[] _logsTypeViews;

        public Dictionary<LogsHeapType, Vector3> LogsHeapTypeToViews { private set; get; } = new();

        public string Id => _guid;
        
        private void Awake()
        {
            LogsHeapTypeToViews = _logsTypeViews.ToDictionary(l => l.type, l => l.logPos.position);
        }

        private void GenerateGuid()
        {
            if (string.IsNullOrWhiteSpace(_guid))
                _guid = Guid.NewGuid().ToString();
            #if UNITY_EDITOR
            EditorUtility.SetDirty(gameObject);
            #endif
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
                size = _size,
                logsCount = _logsCount,
                logsPositions = LogsHeapTypeToViews
            };
        }

        [Serializable]
        private struct LogsTypeView
        {
            public LogsHeapType type;
            public Transform logPos;
        }
    }
}