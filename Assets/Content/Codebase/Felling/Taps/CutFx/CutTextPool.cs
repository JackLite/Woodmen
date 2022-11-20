using UnityEngine;
using Woodman.Utils;

namespace Woodman.Felling.Taps.CutFx
{
    public class CutTextPool : Pool<CutTextView>
    {
        public float lifetime;
        public Transform parent;

        protected override void OnBeforeGet(CutTextView component)
        {
            component.transform.SetParent(parent, true);
            component.transform.localPosition = Vector3.zero;
            component.transform.localScale = Vector3.one;
            component.transform.localRotation = Quaternion.identity;
            component.transform.SetAsLastSibling();
            base.OnBeforeGet(component);
        }
    }
}