using System.Collections.Generic;
using UnityEngine;
using Woodman.Felling.Tree.Progression;
using Woodman.Logs;

namespace Woodman.Felling.Tree
{
    public struct TreeModel
    {
        public int size;
        public float progress;
        public Dictionary<LogsHeapType, Vector3> logsPositions;
        public List<string> createdHeaps;
    }
}