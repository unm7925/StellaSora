using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [Header("Stats")] 
    protected int maxHP = 100;
    [SerializeField]protected int currentHP;
    protected int maxEnergy = 100;
    protected int currentEnergy;


    // 프로퍼티 캡슐화
    public int HP
    {
        get { return currentHP; }
        set { currentHP = Mathf.Clamp(value, 0, maxHP); }
    }

    public int Energy
    {
        get { return currentEnergy; }
        set { currentEnergy = Mathf.Clamp(value, 0, maxEnergy); }
    }

    protected virtual void Start()
    {
        currentHP = maxHP;
        currentEnergy = 0;
    }

    
    public virtual void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + "Die");
    }
    
}
