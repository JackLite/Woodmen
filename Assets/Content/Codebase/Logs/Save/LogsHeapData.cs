using System;
using UnityEngine;

namespace Woodman.Logs.Save
{
    [Serializable]
    public class LogsHeapData
    {
        public string id;
        public LogsHeapType type;
        public int count;
        public Vector3 position;
    }
}