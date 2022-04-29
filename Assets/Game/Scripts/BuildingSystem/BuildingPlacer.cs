using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TerrainCollider))]
public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private List<NoneBuildingArea> _noneBuildingAreas;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Player _player;
    [SerializeField] private float _allowedDistanceToStructures;

    private Building _awaitingPlacement;
    private PlayerInput _playerInput;
    private TerrainCollider _terrainCollider;
    private ShootRangeRenderer _shootRangeRenderer;
    private bool _canBePlaced;
    private bool _isTower;

    public event UnityAction<Building> BuildingPlaced;

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

    private bool CheckPlaceAvailability()
    {
        bool ReturnAvailibility(bool isAvailable)
        {
            _canBePlaced = isAvailable;
            _awaitingPlacement.ColorizeByAvailibility(_canBePlaced);

            return isAvailable;
        }
        _canBePlaced = false;

        if (_awaitingPlacement.IsOnRoad)
            if (_isTower)
                return ReturnAvailibility(false);
        
        if (_isTower == false && _awaitingPlacement.IsOnRoad == false)
            return ReturnAvailibility(false);

        foreach (var structure in Buildings.List)
            if (Vector3.Distance(_awaitingPlacement.transform.position, structure.transform.position) < _allowedDistanceToStructures)
                return ReturnAvailibility(false);

        foreach (var area in _noneBuildingAreas)
            if (area.IsPointInside(_awaitingPlacement.transform))
                return ReturnAvailibility(false);

        return ReturnAvailibility(true);
    }

    private void SetBuildingPlace()
    {
        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (_terrainCollider.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            Vector3 worldPosition = hitInfo.point;

            _awaitingPlacement.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
            CheckPlaceAvailability();
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

        BuildingPlaced?.Invoke(_awaitingPlacement);

        _awaitingPlacement.ResetColor();
        Buildings.List.Add(_awaitingPlacement);
        _awaitingPlacement = null;
    }
}
