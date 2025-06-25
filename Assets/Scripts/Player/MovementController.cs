using UnityEngine;

[RequireComponent (typeof (CharacterController))]
public class MovementController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private InputManager _inputManager;

    [Header("Movement")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = -9.81f;

    [Header("Camera")]
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _upperLimit;
    [SerializeField] private float _bottomLimit;

    private float _xRotation;
    private Vector3 _velocity;

    private void FixedUpdate()
    {
        //if (!_inputManager.IsAlive) return;
        //if (!_inputManager.IsCanMove) return;
        Move();
        //Jump();
        Look();
    }

    private void Move()
    {
        float targetSpeed = _inputManager.Sprint ? _runSpeed : _walkSpeed;
        if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

        _characterController.Move(transform.TransformDirection(new Vector3(_inputManager.Move.normalized.x * targetSpeed, 0, _inputManager.Move.normalized.y * targetSpeed) * Time.fixedDeltaTime));

        if (_characterController.isGrounded && _velocity.y < 0) _velocity.y = -1f;
        _velocity.y += _gravity * Time.fixedDeltaTime;
        _characterController.Move(_velocity * Time.fixedDeltaTime);

    }

    public void Jump()
    {
        if (!_characterController.isGrounded) return;

        _velocity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);
    }

    private void Look()
    {
        float mouseX = _inputManager.Look.x * _sensitivity * Time.fixedDeltaTime;
        float mouseY = _inputManager.Look.y * _sensitivity * Time.fixedDeltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _bottomLimit, _upperLimit);

        transform.Rotate(Vector3.up, mouseX);
        _cameraRoot.localRotation = Quaternion.Euler(_xRotation, 0, 0);
    }
}