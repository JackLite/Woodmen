#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

namespace Woodman.Buildings
{
    public class BuildingStateView : MonoBehaviour
    {
        [SerializeField]
        [ContextMenuItem("Fill", nameof(FillMeshRenderer))]
        private MeshRenderer _meshRenderer;
        
        private Material _material;
        private static readonly int Transparent = Shader.PropertyToID("_Alpha");
        private static readonly int BlinkSlider = Shader.PropertyToID("_BlinkSlider");

        private void Awake()
        {
            if (_meshRenderer == null)
            {
                Debug.LogWarning("No mesh renderer assign at " + gameObject.name);
                _meshRenderer = transform.GetComponentInChildren<MeshRenderer>(true);
            }

            if (_meshRenderer == null)
                return;

            _material = _meshRenderer.material;
        }

        public void ToggleActive(bool state)
        {
            gameObject.SetActive(state);
        }
        
        public void SetTransparency(float t)
        {
            _material.SetFloat(Transparent, t);
        }

        public void SetBlink(float b)
        {
            _material.SetFloat(BlinkSlider, b);
        }

        private void FillMeshRenderer()
        {
            _meshRenderer = transform.GetComponentInChildren<MeshRenderer>(true);
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            #endif
        }
    }
}