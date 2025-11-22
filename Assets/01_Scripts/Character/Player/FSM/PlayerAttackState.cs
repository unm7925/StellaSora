using UnityEngine;

public class PlayerAttackState : IPlayerState
{
    private PlayerStateMachine _stateMachine;
    private float attackDuration = 0.5f;
    private float attackTimer;

    public PlayerAttackState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        _stateMachine.Combat.Attack();
        attackTimer = 0;
    }

    public void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public void Exit()
    {
        
    }
}