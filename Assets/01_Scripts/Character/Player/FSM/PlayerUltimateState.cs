using UnityEngine;

public class PlayerUltimateState : IPlayerState
{
    private PlayerStateMachine _stateMachine;
    private float ultimateDuration = 1f;
    private float ultimateTimer;

    public PlayerUltimateState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    public void Enter()
    {
        _stateMachine.Combat.UseUltimate();
        ultimateTimer = 0;
    }

    public void Update()
    {
        ultimateTimer += Time.deltaTime;

        if (ultimateTimer >= ultimateDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public void Exit()
    {
        
    }
}