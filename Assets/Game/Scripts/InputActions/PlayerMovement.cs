using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _rotateSpeed;

    private Vector2 _zoom;
    private Vector2 _moveDirection;
    private Vector2 _rotation;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _moveDirection = _input.Player.Move.ReadValue<Vector2>();
        _zoom = _input.Player.Zoom.ReadValue<Vector2>();
        _rotation = _input.Player.Rotate.ReadValue<Vector2>();

        Move(_moveDirection);
        Zoom(_zoom);
        Rotate(_rotation);
    }

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = ScaleSpeed(_moveSpeed);
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Zoom(Vector2 direction)
    {
        float scaledZoomSpeed = ScaleSpeed(_zoomSpeed);
        Vector3 zoom = new Vector3(0, -direction.y, direction.y);
        transform.position += zoom * scaledZoomSpeed;
    }

    private void Rotate(Vector2 rotate)
    {
    }

    private float ScaleSpeed(float speed) => speed * Time.deltaTime;
}
