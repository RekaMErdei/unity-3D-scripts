using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    //RequireComponent registered in Unitiy namespace, automatically add a component to a game object
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Vector3 movementVector;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
            Rotate();
        }
        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
            ///agent.Move(movementVector);
        }
        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
        }

        private void Rotate()
        {
            if (movementVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
        }
    }
}

// structure the code
// always the namespace will be the first an below that the classes!!!
// first define the variables like 'NavMeshAgent agent' and 'Vector3 movementVector'
// second define methods like Awake and Update
// finally gets custom methods like MovePlayer, HandleMove and Rotate
// getComponent items could slow down the performance therefore recommended to use awake command!!!
// destnation