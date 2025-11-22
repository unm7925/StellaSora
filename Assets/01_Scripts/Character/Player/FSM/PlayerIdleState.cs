using UnityEngine;

public class PlayerIdleState:IPlayerState
{ 
    private PlayerStateMachine _stateMachine;
    
    public PlayerIdleState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if (_stateMachine.Movement._moveDirection.magnitude > 0.1f)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _stateMachine.ChangeState((_stateMachine.AttackState));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _stateMachine.ChangeState(_stateMachine.DashState);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !_stateMachine.Combat.isSkillCoolDown)
        {
            _stateMachine.ChangeState(_stateMachine.SkillState);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _stateMachine.ChangeState(_stateMachine.UltimateState);
        }
    }

    public void Exit()
    {
        
    }
}
