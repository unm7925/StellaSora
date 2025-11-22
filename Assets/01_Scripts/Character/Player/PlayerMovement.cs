using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 15f;
    
    [Header("Dash")]
    [SerializeField] private float dashDistance = 4f;
    [SerializeField] private float dashDuration  = 0.3f;

    private Rigidbody _rigidbody;
    private Character _character;
    
    public bool isDashing { get; private set; } = false;
    public Vector3 _moveDirection { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _character = GetComponent<Character>();
    }

    public void Move()
    {
        if (isDashing) return;

        if (_moveDirection.magnitude > 0.1f)
        {
            Vector3 velocity = _moveDirection.normalized * moveSpeed;
            _rigidbody.linearVelocity = new Vector3(velocity.x, _rigidbody.linearVelocity.y, velocity.z);
            
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            //transform.forward = _moveDirection;
        }
        else
        {
            _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
        }
    }

    public void StartDash()
    {
        if (!isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        _character.isInvincible = true;

        Vector3 dashDirection = transform.forward;

        float dashSpeed = dashDistance / dashDuration;
        float elapsedTime = 0f;
        float invincibleEndTime = dashDuration * 0.7f;

        while (elapsedTime < dashDuration)
        {
            _rigidbody.linearVelocity = new Vector3(
                dashDirection.x * dashSpeed,
                _rigidbody.linearVelocity.y,
                dashDirection.z * dashSpeed);

            if (elapsedTime >= invincibleEndTime && _character.isInvincible)
            {
                _character.isInvincible = false;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isDashing = false;
        _character.isInvincible = false;
    }
}
