using System;
using UnityEngine;
using RPG.Utility;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public float distanceFromPlayer;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Movement movementCmp;
        [NonSerialized] public GameObject player;
        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();

        private void Awake()
        {
            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);
        }
        private void Update()
        {
            CalculateDistanceFromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distanceFromPlayer = Vector3.Distance(
                enemyPosition, playerPosition
            );
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(
                transform.position,
                chaseRange
            );
        }

    }
}

// getComponent items could slow down the performance therefore recommended to use awake command!!!
// FindWithTag necessary that enemy could recognize the player
// if using System is part of the code we can use [NonSerialized] command to wanish the unnecessary fields from Unity Script fields
// Cmp is short for component
// here add the movement class
// AIBaseState and AIReturnState come from other script as abstract class
// Start method is an other lifecycle method, similar to Awake method but Start method is called once
// prefer to use Wake method to create variables and Start method is great for performing logic
// public AIReturnState returnState = new AIReturnState(); --- new means to create an instance of the state
// this keyword represents the current instance

//private void ChasePlayer()
//{
//   these 2 rows above are moved to the AIChaseState script as a new state class
//   if (distanceFromPlayer > chaseRange) return;
//   movementCmp.MoveAgentByDestination(player.transform.position);
//}

//   currentState = chaseState; --- means that enemy comes automatically to the player