using UnityEngine;

namespace RPG.Character
{
    public class AIChaseState : AIBaseState
    {
        public override void EnterState(EnemyController enemy)
        {
            enemy.movementCmp.UpdateAgentSpeed(enemy.stats.runSpeed, false);
        }
        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distanceFromPlayer > enemy.chaseRange)
            {
                enemy.SwitchState(enemy.returnState);
                return;
            }

            if (enemy.distanceFromPlayer < enemy.attackRange)
            {
                enemy.SwitchState(enemy.attackState);
                return;
            }

            enemy.movementCmp.MoveAgentByDestination(
                enemy.player.transform.position);

            Vector3 playerDirection = enemy.player.transform.position -
                enemy.transform.position;

            enemy.movementCmp.Rotate(playerDirection);

            //Debug.Log(enemy.distanceFromPlayer);


        }
    }
}