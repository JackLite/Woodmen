using System;

namespace Woodman.Buildings
{
    public struct BuildingChangeStateEvent
    {
        public BuildingView buildingView;
        public int newState;
        public Action onFinishBuilding;
    }
}