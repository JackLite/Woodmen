using System;
using UnityEngine;

namespace Woodman.Logs.LogsUsing
{
    public struct UsingLogsCreateEvent
    {
        public int count;
        public float delayBetween;
        public Vector3 from;
        public Vector3 to;
        public Action onAfter;
    }
}