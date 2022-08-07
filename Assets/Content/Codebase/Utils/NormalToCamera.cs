using UnityEngine;

namespace Woodman.Utils
{
    [RequireComponent(typeof(RectTransform))]
    public class NormalToCamera : MonoBehaviour
    {
        private void Start()
        {
            if (Camera.main != null)
                GetComponent<RectTransform>().LookAt(Camera.main.transform);
            else
                Debug.LogError("No camera with tag \"main\"!");
        }
    }
}