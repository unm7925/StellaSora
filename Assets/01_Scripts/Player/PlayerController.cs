using System;
using UnityEngine;

public class PlayerController : Character
{
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody _rigidbody;
    
    private Vector3 _moveDirection;

    protected override void Start()
    {
        base.Start();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }
    

    private void HandleInput()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE    // 키보드
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        #else                                   // 조이스틱
            flaot horizontal = joystick.Horizontal;
            flaot vertical = joystick.Vertical;
        #endif
        
        _moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
    }
    
    private void Move()
    {
        if (_moveDirection.magnitude > 0.1f)
        {
            Vector3 velocity = _moveDirection * moveSpeed;
            _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
        }
        else
        {
            _rigidbody.linearVelocity = new Vector3(0f,_rigidbody.linearVelocity.y,0f);
        }
    }
}
