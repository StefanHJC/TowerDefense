using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _buildingPrice;

    private List<Renderer> _renderers;
    private Material _material;
    private bool _isOnRoad;

    public int Price => _buildingPrice;
    public bool IsOnRoad => _isOnRoad;

    public virtual void Init()
    {
    }

    public void ResetColor()
    {
        _material.color = Color.white;
    }

    public void ColorizeByAvailibility(bool canBePlaced)
    {
        if (canBePlaced)
            _material.color = Color.green;
        else
            _material.color = Color.red;
    }

    private void Awake()
    {
        _renderers = new List<Renderer>();
        GetComponentsInChildren(_renderers);
        _material = new Material(_renderers[0].sharedMaterial);
        _isOnRoad = false;

        foreach (var renderer in _renderers)
            renderer.sharedMaterial = _material;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<WayPoint>(out var _))
            _isOnRoad = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.TryGetComponent<WayPoint>(out var _))
            _isOnRoad = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<WayPoint>(out var _))
            _isOnRoad = false;
    }

}
