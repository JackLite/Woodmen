using UnityEngine;

namespace Woodman.Buildings
{
    public class BuildingVFX : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        public void SetMesh(Mesh mesh)
        {
            var shapeModule = _particleSystem.shape;
            shapeModule.mesh = mesh;
        }

        public void ResetTransform(Transform stateTransform)
        {
            var t = transform;
            t.position = stateTransform.position;
            t.rotation = Quaternion.identity;
            _particleSystem.transform.rotation = stateTransform.rotation;
        }
    }
}