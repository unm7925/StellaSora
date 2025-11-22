using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Character
{
    [Header("Attack")] 
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private Vector3 attackBoxSize = new Vector3(1f, 1f, 1f);
    
    
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Dash")]
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashDuration  = 0.3f;
    
    private bool isDashing = false;
    
    private float invincibleEndTime = 0.7f;

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

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        
        Vector3 dashDirection = transform.forward;
        
        float dashSpeed = dashDistance/dashDuration;
        
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            _rigidbody.linearVelocity = new Vector3(dashDirection.x * dashSpeed,_rigidbody.linearVelocity.y,dashDirection.z*dashSpeed);

            if (elapsedTime >= invincibleEndTime * dashDuration && isInvincible)
            {
                isInvincible = false;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isDashing = false;
        isInvincible = false;
    }

    private void Attack()
    {
        // 애니메이션 ( 나중 )

        Vector3 attackCenter = transform.position + transform.forward * (attackRange / 2);

        Collider[] hits = Physics.OverlapBox(
            attackCenter,
            attackBoxSize / 2,
            transform.rotation);
        foreach (Collider hit in hits)
        {
            if(hit.transform == transform) continue;
            
            IDamageable target = hit.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(attackDamage);
                Energy += 10;
                Debug.Log(Energy);
            }
        }
    }

    private void Move()
    {
        if (isDashing) return;
        
        if (_moveDirection.magnitude > 0.1f)
        {
            Vector3 velocity = _moveDirection * moveSpeed;
            _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
            
            transform.forward = _moveDirection;
        }
        else
        {
            _rigidbody.linearVelocity = new Vector3(0f,_rigidbody.linearVelocity.y,0f);
        }
    }
}
