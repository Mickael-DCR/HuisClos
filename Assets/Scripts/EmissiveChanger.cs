using System;
using UnityEngine;

public class EmissiveChanger : MonoBehaviour
{
    private Material _emissiveMaterial;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _emissiveIntensity;
    [SerializeField] private Color _emissiveColor;

    private void Awake()
    {
        _emissiveMaterial = _renderer.material;
    }
    
    public void ChangeEmissiveIntensity()
    {
        _emissiveMaterial.SetColor("_EmissionColor",  _emissiveColor * _emissiveIntensity);
    }

    public void EnableEmissive()
    {
        _emissiveMaterial.EnableKeyword("_EMISSION");
    }

    public void DisableEmissive()
    {
        _emissiveMaterial.DisableKeyword("_EMISSION");
    }
}
