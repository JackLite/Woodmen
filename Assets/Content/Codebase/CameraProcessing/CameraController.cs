namespace Woodman.CameraProcessing
{
    public class CameraController
    {
        private readonly CamerasContainer _camerasContainer;
        public CameraController(CamerasContainer camerasContainer)
        {
            _camerasContainer = camerasContainer;
        }

        public void MoveToCore()
        {
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