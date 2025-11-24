using UnityEngine;
using UnityEngine.InputSystem;

public class PosNoSeComoLlamarle : MonoBehaviour
{
    
    Animator _animator;
    CharacterController _controller;

    InputAction _jumpAction;
    InputAction _moveAction;

    Vector2 _moveValue;

    [SerializeField] private float _movementSpeed = 5;

    [SerializeField] private float _jumpHeight = 2;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] Transform _sensorPosition;
    [SerializeField] float _sensorRadius;
    [SerializeField] LayerMask _groundLayer;
    Vector3 _playerGravity;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        _jumpAction = InputSystem.actions["Jump"];
        _moveAction = InputSystem.actions["Move"];
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        _moveValue = _moveAction.ReadValue<Vector2>();
        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        Movement();
        Gravity();
    }

    void Movement()
    {
        Vector3 moveDirection = new Vector3(_moveValue.x, 0, _moveValue.y);
        _controller.Move(moveDirection * _movementSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
    }

    void Jump()
    {
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * _gravity * -2);
        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Gravity()
    {
        if(!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
            
        }
        else if(IsGrounded() && _playerGravity.y < 0)
        {
            _playerGravity.y = _gravity;
        }
        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sensorPosition.position, _sensorRadius);
    }
}
