using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : Character
{
    private PlayerStateMachine _playerStateMachine;
    private PlayerMovement _playerMovement;
    
    private Vector3 moveDirection;
    
    protected override void Start()
    {
        base.Start();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        _playerMovement.Move();
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
        
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        _playerMovement._moveDirection = moveDirection;
    }

    
    
   
}
