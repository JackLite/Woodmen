using Woodman.MetaInteractions;
using Woodman.PlayerRes;

namespace Woodman.Buildings
{
    public class BuildingInteraction
    {
        private readonly PlayerResRepository _resRepository;

        public BuildingInteraction(PlayerResRepository resRepository)
        {
            _resRepository = resRepository;
        }

        public void OnInteract(InteractTarget target)
        {
            var interact = target.GetComponent<BuildingInteract>();
            if (interact == null)
                return;

            //todo: вычислять на основе сохранки через отдельный сервис
            var nextStateIndex = 1; 
            var nextCount = interact.BuildingView.GetResForState(nextStateIndex);
            if (nextCount > _resRepository.GetPlayerRes())
                return;

            _resRepository.SubtractRes(nextCount);
            // todo: брать индекс на основе сохранки через отдельный сервис
            interact.BuildingView.SetState(1); 
        }
    }
}