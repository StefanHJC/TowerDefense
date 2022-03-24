using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Building : MonoBehaviour
{
    private List<Renderer> _renderers;
    private Material _material;

    private void Awake()
    {
        _renderers = new List<Renderer>();
        GetComponentsInChildren(_renderers);
        _material = new Material(_renderers[0].sharedMaterial);

        foreach (var renderer in _renderers)
            renderer.sharedMaterial = _material;
    }

    public void ResetColor()
    {
        _material.color = Color.white;
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }
}
