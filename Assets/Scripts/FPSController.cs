using UnityEngine;
using UnityEngine.InputSystem;

using Unity.Cinemachine;
using Unity.Mathematics;

public class FPSController : MonoBehaviour
{
    //Componentes
    private CharacterController _controller;
    private Animator _animator;

    //Inputs
    private InputAction _moveAction;
    private Vector2 _moveInput;
    [SerializeField] private Vector2 _lookInput;
    private InputAction _jumpAction;
    private InputAction _lookAction;
    private InputAction _aimAction;
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _jumpHeight = 2;

    //Camera
    private Transform _mainCamera;
    [SerializeField] private float _cameraSensitivity = 10;
    [SerializeField] Transform _lookAtCamera;
    float _xRotation;

    //Ground Sensor
    [SerializeField] Transform _sensor;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _sensorRadius = 4;

    //Gravedad
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _playerGravity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _mainCamera = Camera.main.transform;

        _moveAction = InputSystem.actions["Move"];
        _lookAction = InputSystem.actions["Look"];
        _jumpAction = InputSystem.actions["Jump"];
        _aimAction = InputSystem.actions["Aim"];
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        Movement();
        if (_jumpAction.WasPerformedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Gravity();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y);

        float mouseX = _lookInput.x * _cameraSensitivity * Time.deltaTime;
        float mouseY = _lookInput.y * _cameraSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -89, 89);

        _animator.SetFloat("Vertical", _moveInput.x);
        _animator.SetFloat("Horizontal", _moveInput.y);

        transform.Rotate(Vector3.up, mouseX);
        _lookAtCamera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        //_lookAtCamera.Rotate(Vector3.right, mouseY);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
        }

    }

    void Jump()
    {

        _animator.SetBool("IsJumping", true);
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Gravity()
    {

        if (!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }
        else if (IsGrounded() && _playerGravity.y < 0)
        {
            _animator.SetBool("IsJumping", false);
            _playerGravity.y = -9.81f;
        }
        

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensor.position, _sensorRadius, _groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_sensor.position, _sensorRadius);
    }


}
