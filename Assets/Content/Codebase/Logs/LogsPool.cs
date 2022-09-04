using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Woodman.Utils;
using Logger = Woodman.Utils.Logger;

namespace Woodman.Logs
{
    public class LogsPool : MonoBehaviour
    {
        [SerializeField]
        private PoolByType[] _pools;

        public async Task WarmUp(LogsHeapType type, int count)
        {
            var pool = FindPool(type);
            if (pool)
                await pool.WarmUp(count);
        }

        public LogView GetLogView(LogsHeapType type)
        {
            var pool = FindPool(type);
            return pool ? pool.Get() : null;
        }

        public void Return(LogView logView)
        {
            var pool = FindPool(logView.LogType);
            if (pool)
                pool.Return(logView);
        }

        private Pool<LogView> FindPool(LogsHeapType type)
        {
            var poolByType = _pools.FirstOrDefault(p => p.type == type);
            if (poolByType.pool != null) 
                return poolByType.pool;
            
            Logger.LogError(this, nameof(WarmUp), $"Not found pool by type {type}");
            return null;

        }

        [Serializable]
        private struct PoolByType
        {
            public LogsHeapType type;
            public LogsInnerPool pool;
        }
    }
}