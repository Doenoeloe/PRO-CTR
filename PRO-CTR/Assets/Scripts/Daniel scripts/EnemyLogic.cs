using UnityEngine;
using UnityEngine.UI;

namespace Daniel_scripts
{
    public class EnemyLogic : TurnActor
    {
        [SerializeField] private EnemyMovement movement;
        private float totalHealth = 50;

        

        private void Awake()
        {
            if (movement == null)
                movement = GetComponent<EnemyMovement>();

            if (movement != null)
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