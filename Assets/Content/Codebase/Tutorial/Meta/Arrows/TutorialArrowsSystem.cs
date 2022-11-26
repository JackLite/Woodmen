using ModulesFramework;
using ModulesFramework.Attributes;
using ModulesFramework.Data;
using ModulesFramework.Systems;
using Woodman.Locations;
using Woodman.Locations.Interactions;
using Woodman.Locations.Interactions.Components;

namespace Woodman.Tutorial.Meta.Arrows
{
    [EcsSystem(typeof(MetaTutorialModule))]
    public class TutorialArrowsSystem : IActivateSystem, IRunSystem
    {
        private DataWorld _world;
        private EcsOneData<MetaTutorialData> _tutorialData;
        private EcsOneData<LocationData> _locationData;

        public void Activate()
        {
            ref var td = ref _tutorialData.GetData();
            ref var ld = ref _locationData.GetData();
            ld.tutorialArrowsProvider.logArrow.Hide();
            ld.tutorialArrowsProvider.buildArrow.Hide();
            if (td.tutorialComplete || !td.firstStepComplete)
                return;
            if (!td.secondStepComplete)
                ld.tutorialArrowsProvider.logArrow.Show();
            else if (!td.thirdStepComplete)
                ld.tutorialArrowsProvider.buildArrow.Show();
        }

        public void Run()
        {
            CheckLogArrow();
            CheckBuildArrow();
        }

        private void CheckLogArrow()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.secondStepComplete)
                return;

            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Logs);

            if (!q.Any())
                return;
            td.secondStepComplete = true;
            td.isDirty = true;
            ref var ld = ref _locationData.GetData();
            ld.tutorialArrowsProvider.logArrow.Hide();
            ld.tutorialArrowsProvider.buildArrow.Show();
        }
        
        private void CheckBuildArrow()
        {
            ref var td = ref _tutorialData.GetData();
            if (td.thirdStepComplete)
                return;

            var q = _world.Select<Interact>()
                .Where<Interact>(i => i.interactType == InteractTypeEnum.Building);

            if (!q.Any())
                return;
            td.thirdStepComplete = true;
            td.isDirty = true;
            ref var ld = ref _locationData.GetData();
            ld.tutorialArrowsProvider.buildArrow.Hide();
        }
    }
}