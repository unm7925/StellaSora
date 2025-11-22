using System;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private int attackDamage = 10;
    
    
    protected override void Start()
    {
        base.Start();
    }

    private void OnCollisionStay(Collision other)
    {
        IDamageable target = other.gameObject.GetComponent<IDamageable>();
        if (target != null && other.gameObject.CompareTag(ConfigManager.Config.playerTag))
        {
            target.TakeDamage((attackDamage));
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        Debug.Log(gameObject.name + " attack " + HP);
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
