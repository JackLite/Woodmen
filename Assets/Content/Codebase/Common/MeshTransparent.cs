using UnityEngine;

namespace Woodman.Common
{
    /// <summary>
    /// Отвечает за смену прозрачности у деревьев и домиков
    /// </summary>
    public class MeshTransparent : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _meshRenderer;

        private bool _isChanged;
        private Material _material;
        private static readonly int Alpha = Shader.PropertyToID("_Alpha");

        private void Awake()
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();
            _material = _meshRenderer.material;
        }

        public void SetTransparency(float transparency)
        {
            _material.SetFloat(Alpha, transparency);
        }
    }
}