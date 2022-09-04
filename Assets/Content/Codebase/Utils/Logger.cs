using UnityEngine;

namespace Woodman.Utils
{
    public static class Logger
    {
        public static void LogError(string className, string methodName, string msg)
        {
            Debug.LogError($"[{className}::{methodName}] {msg}");
        }
        
        public static void LogError(object sourceObject, string methodName, string msg)
        {
            Debug.LogError($"[{sourceObject.GetType().Name}::{methodName}] {msg}");
        }
    }
}