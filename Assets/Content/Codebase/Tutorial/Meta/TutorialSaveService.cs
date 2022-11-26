using UnityEngine;
using Woodman.Tutorial.Core;
using Woodman.Utils;

namespace Woodman.Tutorial.Meta
{
    public class TutorialSaveService
    {
        private const string SaveMetaKey = "tutorial.meta.data";
        private const string SaveCoreKey = "tutorial.core.data";

        public void Save(MetaTutorialData data)
        {
            SaveUtility.SaveString(SaveMetaKey, JsonUtility.ToJson(data), true);
        }

        public void Save(CoreTutorialData data)
        {
            SaveUtility.SaveString(SaveCoreKey, JsonUtility.ToJson(data), true);
        }

        public MetaTutorialData LoadMetaData()
        {
            if (!SaveUtility.IsKeyExist(SaveMetaKey))
                return new MetaTutorialData();
            return JsonUtility.FromJson<MetaTutorialData>(SaveUtility.LoadString(SaveMetaKey));
        }

        public CoreTutorialData LoadCoreData()
        {
            if (!SaveUtility.IsKeyExist(SaveCoreKey))
                return new CoreTutorialData();
            return JsonUtility.FromJson<CoreTutorialData>(SaveUtility.LoadString(SaveCoreKey));
        }
    }
}