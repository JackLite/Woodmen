using UnityEngine;
using Woodman.Utils;

namespace Woodman.Buildings
{
    public class BuildingFxPool : Pool<Transform>
    {
        protected override void ResetPoolObject(Transform t)
        {
            t.gameObject.SetActive(false);
        }

        protected override void OnBeforeGet(Transform t)
        {
            t.gameObject.SetActive(true);
        }
    }
}