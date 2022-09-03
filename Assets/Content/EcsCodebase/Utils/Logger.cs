using UnityEngine;

namespace Woodman.EcsCodebase.Utils
{
    public static class Logger
    {
        public static void LogError(string className, string methodName, string msg)
        {
            Debug.LogError($"[{className}::{methodName}] {msg}");
        }
    }
}