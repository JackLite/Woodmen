using System;
using System.Collections.Generic;

namespace Woodman.Buildings
{
    [Serializable]
    public class BuildingSave
    {
        public Dictionary<string, BuildingData> building = new();
    }
}