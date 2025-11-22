using UnityEngine;

public class PlayerSkillState : IPlayerState
{
    private PlayerStateMachine _stateMachine;
    private float skillDuration = 0.7f;
    private float skillTimer;

    public PlayerSkillState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        _stateMachine.Combat.UseSkill();
        skillTimer = 0;
    }

    public void Update()
    {
        skillTimer += Time.deltaTime;

        if (skillTimer >= skillDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public void Exit()
    {
        
    }
}