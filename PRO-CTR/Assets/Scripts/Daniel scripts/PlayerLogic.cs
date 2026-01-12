using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public DefendState defendState;
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public MoveState moveState;
    
    PlayerTurnStates currentState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackState = new AttackState(this);
        defendState = new DefendState(this);
        idleState = new IdleState(this);
        moveState = new MoveState(this);
        
        currentState = idleState;
    }

    public void ChangeState(PlayerTurnStates newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
