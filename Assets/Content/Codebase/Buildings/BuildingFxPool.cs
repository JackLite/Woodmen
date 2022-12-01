using UnityEngine;
using Woodman.Utils;

namespace Woodman.Buildings
{
    public class BuildingFxPool : Pool<BuildingVFX>
    {
        protected override void ResetPoolObject(BuildingVFX t)
        {
            t.gameObject.SetActive(false);
        }

        protected override void OnBeforeGet(BuildingVFX t)
        {
            t.gameObject.SetActive(true);
        }
    }
}