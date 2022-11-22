using UnityEngine;
using Woodman.Utils;

namespace Woodman.Tutorial
{
    public class TutorialSaveService
    {
        private const string SaveKey = "tutorial.data";

        public void Save(TutorialData data)
        {
            SaveUtility.SaveString(SaveKey, JsonUtility.ToJson(data), true);
        }

        public TutorialData LoadData()
        {
            if (!SaveUtility.IsKeyExist(SaveKey))
                return new TutorialData();
            return JsonUtility.FromJson<TutorialData>(SaveUtility.LoadString(SaveKey));
        }
    }
}