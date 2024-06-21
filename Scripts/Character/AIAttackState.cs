using UnityEngine;

namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();

            // Debug.Log("Entering attack state");
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Debug.Log("Attacking player");

        }
    }
}


/* namespace RPG.Character
{
    public class AIAttackState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.StopMovingAgent();

            // Debug.Log("Entering attack state");
        }
        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.attackRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Debug.Log("Attacking player");

        }
    }
}
*/