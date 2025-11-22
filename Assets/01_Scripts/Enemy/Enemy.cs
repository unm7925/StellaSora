using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        base.Start();
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
