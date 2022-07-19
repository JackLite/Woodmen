using Cinemachine;
using UnityEngine;

namespace Woodman.CameraProcessing
{
    public class CamerasContainer : MonoBehaviour
    {
        [field:SerializeField]
        public CinemachineVirtualCamera MetaCamera;

        [field:SerializeField]
        public CinemachineVirtualCamera CoreCamera;
    }
}