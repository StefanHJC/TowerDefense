using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private List<NoneBuildingArea> _noneBuildingAreas;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _allowedDistanceToWaypoints;
    [SerializeField] private float _allowedDistanceToStructures;

    private Building _awaitingPlacement;
    private PlayerInput _playerInput;
    private TerrainCollider _terrainCollider;
    private bool _canBePlaced;

    public void StartPlacing(Building prefab)
    {
        if (_awaitingPlacement != null)
            Destroy(_awaitingPlacement.gameObject);

        _awaitingPlacement = Instantiate(prefab);
    }

    public void CancelPlacing()
    {
        if (_awaitingPlacement != null)
            Destroy(_awaitingPlacement.gameObject);
    }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Player.MouseLeftButtonClicked.performed += ctx => TryPlaceBuilding();
        _playerInput.Player.MouseRightButtonClicked.performed += ctx => CancelPlacing();

        _terrainCollider = GetComponent<TerrainCollider>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        if (_awaitingPlacement != null)
            SetBuildingPlace();
    }

    private bool CheckPlaceAvailibility(Building building)
    {
        bool NotAvailable()
        {
            building.SetColor(Color.red);
            _canBePlaced = false;
            return false;
        }
        _canBePlaced = true;

        foreach (var waypoint in WayPoints.List)
            if (Vector3.Distance(_awaitingPlacement.transform.position, waypoint.transform.position) < _allowedDistanceToWaypoints)
                return NotAvailable();

        foreach (var area in _noneBuildingAreas)
            if (area.ReturnIsPointInside(_awaitingPlacement.transform))
                return NotAvailable();

        building.SetColor(Color.green);
        return true;
    }

    private void SetBuildingPlace()
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (_terrainCollider.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            Vector3 worldPosition = hitInfo.point;

            _awaitingPlacement.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            CheckPlaceAvailibility(_awaitingPlacement);
        }
    }

    private void TryPlaceBuilding()
    {
        if (_awaitingPlacement == null || _canBePlaced == false)
            return;

        _awaitingPlacement.ResetColor();
        _awaitingPlacement = null;
    }
}
