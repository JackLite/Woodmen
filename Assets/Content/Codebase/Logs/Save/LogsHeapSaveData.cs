using System;
using System.Collections.Generic;

namespace Woodman.Logs.Save
{
    [Serializable]
    public class LogsHeapSaveData
    {
        public Dictionary<string, LogsHeapData> logHeaps = new();
    }
}