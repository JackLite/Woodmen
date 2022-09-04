using System;
using Newtonsoft.Json;

namespace Woodman.Utils
{
    public static class RepositoryHelper
    {
        public static T CreateSaveData<T>(string saveKey) where T : class, new()
        {
            if (!SaveUtility.IsKeyExist(saveKey))
                return new T();

            try
            {
                var json = SaveUtility.LoadString(saveKey);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Logger.LogError(nameof(RepositoryHelper), nameof(CreateSaveData), e.Message + e.StackTrace);
                return new T();
            }
        }

        public static void Save<T>(string saveKey, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            SaveUtility.SaveString(saveKey, json, true);
        }
    }
}