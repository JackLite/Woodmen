using UnityEngine;

namespace Woodman.CameraProcessing
{
    public class CameraController
    {
        private readonly CamerasContainer _camerasContainer;
        public CameraController(CamerasContainer camerasContainer)
        {
            _camerasContainer = camerasContainer;
        }

        public void MoveToCore(Transform focusTarget)
        {
            _camerasContainer.CoreCamera.transform.position = _camerasContainer.MetaCamera.transform.position;
            _camerasContainer.CoreCamera.LookAt = focusTarget;
            _camerasContainer.CoreCamera.Follow = focusTarget;
            _camerasContainer.MetaCamera.enabled = false;
            _camerasContainer.CoreCamera.enabled = true;
        }
        
        public void MoveToMeta()
        {
            _camerasContainer.MetaCamera.enabled = true;
            _camerasContainer.CoreCamera.enabled = false;
        }
    }
}