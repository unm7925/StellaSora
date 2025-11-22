using System;
using UnityEngine;
using UnityEngine.XR;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    
    // 컴포넌트 참조
    public PlayerController Controller { get; private set; }
    public PlayerMovement Movement { get; private set; }
    public PlayerCombat Combat { get; private set; }
    public Character Character { get; private set; }
    
    // 상태
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState {get; private set;}
    public PlayerSkillState SkillState { get; private set; }
    public PlayerUltimateState UltimateState { get; private set; }

    private void Awake()
    {
        //컴포넌트
        Controller = GetComponent<PlayerController>();
        Movement = GetComponent<PlayerMovement>();
        Combat = GetComponent<PlayerCombat>();
        Character = GetComponent<Character>();
        
        
        // 상태
        IdleState = new PlayerIdleState(this);
        MoveState = new PlayerMoveState(this);
        DashState = new PlayerDashState(this);
        AttackState = new PlayerAttackState(this);
        SkillState = new PlayerSkillState(this);
        UltimateState = new PlayerUltimateState(this);
    }

    private void Start()
    {
       ChangeState(IdleState);
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(IPlayerState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
        
        
    }
}
