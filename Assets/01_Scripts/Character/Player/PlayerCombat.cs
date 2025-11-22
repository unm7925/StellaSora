using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Attack")] 
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private Vector3 attackBoxSize = new Vector3(1f, 1f, 1f);

    [Header("Skill")] 
    [SerializeField] private int skillDamage = 20;
    [SerializeField] private float skillCooldown = 5f;
    [SerializeField] private float skillRange = 4f;
    [SerializeField] private Vector3 skillBoxSize = new Vector3(3f, 3f, 3f);
    
    [Header("Ultimate")]
    [SerializeField] private int ultimateDamage = 40;
    [SerializeField] private int ultimateEnergyCost = 100;
    [SerializeField] private float ultimateRange = 7f;
    [SerializeField] private Vector3 ultimateBoxSize = new Vector3(6f, 6f, 6f);
    
    private Character _character;

    public bool isSkillCoolDown { get; private set; } = false;

    private void Start()
    {
        _character = GetComponent<Character>();
    }

    public void Attack()
    {
        Vector3 attackCenter = transform.position + transform.forward * (attackRange / 2);

        Collider[] hits = Physics.OverlapBox(
            attackCenter,
            attackBoxSize / 2,
            transform.rotation);

        foreach (Collider hit in hits)
        {
            if(hit.transform == transform)continue;
            
            IDamageable target = hit.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(attackDamage);
                _character.Energy += 20;
            }
        }
    }

    public void UseSkill()
    {
        Vector3 skillCenter = transform.position + transform.forward * (skillRange / 2);
        
        Collider[] hits = Physics.OverlapBox(skillCenter,
            skillBoxSize / 2,
            transform.rotation);
        
        foreach (Collider hit in hits)
        { 
            if (hit.transform == transform) continue;
            IDamageable target = hit.GetComponent<IDamageable>();
            
            if (target != null) 
            {
                target.TakeDamage(skillDamage); 
            }
        }

        StartCoroutine(SkillCooldown());
    }

    private IEnumerator SkillCooldown()
    {
        isSkillCoolDown = true;
        
        yield return new WaitForSeconds(skillCooldown);
        isSkillCoolDown = false;
    }

    public void UseUltimate()
    {
        if (_character.Energy < ultimateEnergyCost) return;

        _character.Energy -= ultimateEnergyCost;

        Vector3 ultimateCenter = transform.position + transform.forward * (ultimateRange / 2);

        Collider[] hits = Physics.OverlapBox(
            ultimateCenter,
            ultimateBoxSize / 2,
            transform.rotation);

        foreach (Collider hit in hits)
        {
            if(hit.transform == transform) continue;
            
            IDamageable target = hit.GetComponent<IDamageable>();
            
            if (target != null)
            {
                target.TakeDamage(ultimateDamage); 
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        // 공격 범위 (빨강)
        Gizmos.color = Color.red;
        Vector3 attackCenter = transform.position + transform.forward * (attackRange / 2);
        Gizmos.DrawWireCube(attackCenter, attackBoxSize);
    
        // 스킬 범위 (파랑)
        Gizmos.color = Color.blue;
        Vector3 skillCenter = transform.position + transform.forward * (skillRange / 2);
        Gizmos.DrawWireCube(skillCenter, skillBoxSize);
    
        // 필살기 범위 (노랑)
        Gizmos.color = Color.yellow;
        Vector3 ultimateCenter = transform.position + transform.forward * (ultimateRange / 2);
        Gizmos.DrawWireCube(ultimateCenter, ultimateBoxSize);
    }
}
