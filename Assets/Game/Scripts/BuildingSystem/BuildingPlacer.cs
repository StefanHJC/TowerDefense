using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private float _allowedDistanceToWaypoints;
    [SerializeField] private float _allowedDistanceToStructures;

    private Building _awaitingPlacement;
    private Camera _mainCamera;
    private PlayerInput _playerInput;

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


        _mainCamera = Camera.main;
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
        foreach(var waypoint in WayPoints.List)
            if (Vector3.Distance(_awaitingPlacement.transform.position, waypoint.transform.position) < _allowedDistanceToWaypoints)
            {
                building.SetColor(Color.red);
                return false;
            }

        building.SetColor(Color.green);
        return true;
    }

    private void SetBuildingPlace()
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            _awaitingPlacement.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
            CheckPlaceAvailibility(_awaitingPlacement);
        }
    }

    private void TryPlaceBuilding()
    {
        if (_awaitingPlacement == null)
            return;

        _awaitingPlacement.ResetColor();
        _awaitingPlacement = null;
    }
}
