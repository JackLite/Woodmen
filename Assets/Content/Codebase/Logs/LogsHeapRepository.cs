using System;
using System.Collections.Generic;
using UnityEngine;
using Woodman.Logs.Save;
using Woodman.Utils;

namespace Woodman.Logs
{
    public class LogsHeapRepository
    {
        private const string SAVE_KEY = "meta.logs";
        private readonly LogsHeapSaveData _saveData;

        public LogsHeapRepository()
        {
            _saveData = RepositoryHelper.CreateSaveData<LogsHeapSaveData>(SAVE_KEY);
        }

        public IEnumerable<LogsHeapData> GetData()
        {
            foreach (var (_, data) in _saveData.logHeaps)
            {
                yield return data;
            }
        }

        public void SetCount(string id, int count)
        {
            Check(id);
            if (count <= 0)
                _saveData.logHeaps.Remove(id);
            else
                _saveData.logHeaps[id].count = count;

            Save();
        }

        public string Create(LogsHeapType heapType, int count, Vector3 pos)
        {
            var id = Guid.NewGuid().ToString();
            _saveData.logHeaps[id] = new LogsHeapData
            {
                id = id,
                count = count,
                type = heapType,
                position = pos
            };
            Save();
            return id;
        }

        private void Check(string id)
        {
            if (_saveData.logHeaps.ContainsKey(id))
                return;
            _saveData.logHeaps[id] = new LogsHeapData
            {
                count = 0,
                id = id,
                position = Vector3.zero,
                type = LogsHeapType.Middle
            };
        }

        private void Save()
        {
            RepositoryHelper.Save(SAVE_KEY, _saveData);
        }
    }
}