using UnityEngine;

public class MoveState : PlayerTurnStates
{
    PlayerMovement playerMovement;

    public MoveState(PlayerLogic player) : base(player)
    {
        this.player = player;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public override void Enter()
    {
        playerMovement.CanMove = true;
        playerMovement.OnMoveFinished += HandleMoveFinished;
    }

    public override void Exit()
    {
        playerMovement.CanMove = false;
        playerMovement.OnMoveFinished -= HandleMoveFinished;
    }
    private void HandleMoveFinished()
    {
        player.EndTurn(); // or ChangeState(AttackState) if you want phases
    }
}
