using UnityEngine;

public class PlayerDashState:IPlayerState
{ 
    private PlayerStateMachine _stateMachine;
    
    public PlayerDashState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        _stateMachine.Movement.StartDash();
    }

    public void Update()
    {
        if (!_stateMachine.Movement.isDashing)
        {
            _stateMachine.ChangeState((_stateMachine.IdleState));
        }
    }

    public void Exit()
    {
        
    }
}
