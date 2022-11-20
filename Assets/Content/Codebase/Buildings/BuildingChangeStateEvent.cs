namespace Woodman.Buildings
{
    public struct BuildingChangeStateEvent
    {
        public BuildingView buildingView;
        public int newState;
    }
}