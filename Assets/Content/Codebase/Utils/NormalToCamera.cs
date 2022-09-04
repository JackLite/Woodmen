using UnityEngine;

namespace Woodman.Utils
{
    [RequireComponent(typeof(RectTransform))]
    [ExecuteAlways]
    public class NormalToCamera : MonoBehaviour
    {
        [SerializeField]
        private bool _isUpdate;

        private Camera _mainCamera;
        private RectTransform _rect;

        #region unity

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            LookToCamera();
        }

        private void Update()
        {
            if (!_isUpdate)
                return;
            LookToCamera();
        }

        #endregion
        
        private void LookToCamera()
        {
            if (_mainCamera != null)
                _rect.LookAt(_mainCamera.transform, Vector3.up);
            else
                Debug.LogError("No camera with tag \"main\"!");
        }
    }
}