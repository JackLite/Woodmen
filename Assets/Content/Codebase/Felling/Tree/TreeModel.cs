using System.Collections.Generic;
using UnityEngine;
using Woodman.Logs;

namespace Woodman.Felling.Tree
{
    public struct TreeModel
    {
        public int size;
        public Dictionary<LogsHeapType, Vector3> logsPositions;
    }
}