using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _buildingPrice;

    private List<Renderer> _renderers;
    private Material _material;

    public int Price => _buildingPrice;

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
