using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private List<NoneBuildingArea> _noneBuildingAreas;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _allowedDistanceToWaypoints;
    [SerializeField] private float _allowedDistanceToStructures;
    [SerializeField] private Player _player;

    private Building _awaitingPlacement;
    private PlayerInput _playerInput;
    private TerrainCollider _terrainCollider;
    private ShootRangeRenderer _shootRangeRenderer;
    private bool _canBePlaced;
    private bool _isTower;

    public void StartPlacing(Building prefab)
    {
        if (_awaitingPlacement != null)
            Destroy(_awaitingPlacement.gameObject);

        _awaitingPlacement = Instantiate(prefab);
        _isTower = false;

        if (_awaitingPlacement.TryGetComponent<Tower>(out var tower))
        {
            _shootRangeRenderer = _awaitingPlacement.gameObject.AddComponent<ShootRangeRenderer>();
            _shootRangeRenderer.Init(tower.TowerStats.ShootRange);
            _isTower = true;
        }
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

    private bool CheckPlaceAvailability(Building building)
    {
        bool NotAvailable()
        {
            building.SetColor(Color.red);
            _canBePlaced = false;

            return false;
        }

        bool Available()
        {
            building.SetColor(Color.green);
            _canBePlaced = true;

            return true;
        }

        if (_isTower)
            foreach (var waypoint in WayPoints.List)
                if (Vector3.Distance(_awaitingPlacement.transform.position, waypoint.transform.position) < _allowedDistanceToWaypoints)
                    return NotAvailable();

        foreach (var structure in Buildings.List)
            if (Vector3.Distance(_awaitingPlacement.transform.position, structure.transform.position) < _allowedDistanceToStructures)
                return NotAvailable();

        foreach (var area in _noneBuildingAreas)
            if (area.ReturnIsPointInside(_awaitingPlacement.transform))
                return NotAvailable();

        return Available();
    }

    private void SetBuildingPlace()
    {
        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (_terrainCollider.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            Vector3 worldPosition = hitInfo.point;

            _awaitingPlacement.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            CheckPlaceAvailability(_awaitingPlacement);
        }
    }

    private void TryPlaceBuilding()
    {
        if (_awaitingPlacement == null || _canBePlaced == false)
            return;

        if (_player.Gold - _awaitingPlacement.Price < 0)
        {
            Debug.Log("Not enough gold");
            return;
        }

        if (_shootRangeRenderer != null)
            _shootRangeRenderer.Disable();

        _player.ReduceGold(_awaitingPlacement.Price);
        _awaitingPlacement.ResetColor();
        Buildings.List.Add(_awaitingPlacement);
        _awaitingPlacement = null;
    }
}
