using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Woodman.Felling.Tree;
using Woodman.Logs;

namespace Woodman.Locations.Trees
{
    public class TreeMeta : MonoBehaviour
    {
        [ContextMenuItem("Generate guid", nameof(GenerateGuid))]
        [SerializeField]
        private string _guid;

        [SerializeField]
        private GameObject _tree;

        [SerializeField]
        private LogsTypeView[] _logsTypeViews;

        [SerializeField]
        private GameObject _stump;

        private Dictionary<LogsHeapType, Vector3> LogsHeapTypeToViews { set; get; } = new();

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
            EditorUtility.SetDirty(this);
            #endif
        }
        
        public void ShowTree()
        {
            _tree.SetActive(true);
            _stump.SetActive(false);
        }

        public void ShowStump()
        {
            _tree.SetActive(false);
            _stump.SetActive(true);
        }

        public TreeModel GetTreeModel()
        {
            return new TreeModel
            {
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