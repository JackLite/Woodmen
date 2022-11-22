using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;

namespace Woodman.Tutorial
{
    [EcsSystem(typeof(MetaModule))]
    public class TutorialInitSystem : IInitSystem, IActivateSystem
    {
        private DataWorld _world;
        private EcsOneData<TutorialData> _tutorialData;

        public void Init()
        {
            if (_tutorialData.GetData().tutorialComplete)
                return;

            _world.InitModule<TutorialModule, MetaModule>();
        }

        public void Activate()
        {
            if (_tutorialData.GetData().tutorialComplete)
                return;
            _world.ActivateModule<TutorialModule>();
        }
    }
}