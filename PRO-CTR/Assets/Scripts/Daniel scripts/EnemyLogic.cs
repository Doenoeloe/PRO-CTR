using UnityEngine;

namespace Daniel_scripts
{
    public class EnemyLogic : TurnActor
    {
        [SerializeField] private EnemyMovement movement;

        private void Awake()
        {
            if (movement == null)
                movement = GetComponent<EnemyMovement>();

            movement.OnMoveFinished += EndTurn;
        }

        public override void StartTurn()
        {
            enabled = true;
            movement.StartEnemyTurn();
        }

        private void EndTurn()
        {
            OnTurnFinished?.Invoke();
            enabled = false;
        }
    }
}