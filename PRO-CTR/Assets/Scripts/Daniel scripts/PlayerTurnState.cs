using UnityEngine;

public abstract class PlayerTurnStates
{
    protected PlayerLogic player;

    protected PlayerTurnStates(PlayerLogic player)
    {
        this.player = player;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Tick()
    {
        
    }
    
    public virtual void Exit()
    {
        
    }
}
