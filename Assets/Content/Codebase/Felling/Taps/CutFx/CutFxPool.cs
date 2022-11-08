using UnityEngine;
using Woodman.Utils;

namespace Woodman.Felling.Taps.CutFx
{
    public class CutFxPool : Pool<Transform>
    {
        public float lifetime = 2f;
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