using UnityEngine;

namespace Woodman.Common
{
    /// <summary>
    /// Отвечает за смену прозрачности у деревьев и домиков
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshTransparent : MonoBehaviour
    {
        private bool _isChanged;
        private Material _material;
        private static readonly int Alpha = Shader.PropertyToID("_Alpha");

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        public void SetTransparency(float transparency)
        {
            // if (!_isChanged)
            //     CopyMaterial();
            _material.SetFloat(Alpha, transparency);
        }
    }
}