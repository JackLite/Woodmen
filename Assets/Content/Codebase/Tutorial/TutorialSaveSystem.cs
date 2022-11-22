using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Systems;

namespace Woodman.Tutorial
{
    [EcsSystem(typeof(TutorialModule))]
    public class TutorialSaveSystem : IPostRunSystem, IDestroySystem
    {
        private EcsOneData<TutorialData> _tutorialData;
        private TutorialSaveService _saveService;
        
        public void PostRun()
        {
            SaveIfNeed();
        }

        private void SaveIfNeed()
        {
            ref var td = ref _tutorialData.GetData();
            if (!td.isDirty)
                return;
            td.isDirty = false;
            _saveService.Save(td);
        }

        public void Destroy()
        {
            SaveIfNeed();
        }
    }
}