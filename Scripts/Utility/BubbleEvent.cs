using UnityEngine;
using UnityEngine.Events;

namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => { };
        public event UnityAction OnBubbleCompleteAttack = () => { };
        public event UnityAction OnBubbleHit = () => { };
        public event UnityAction OnBubbleCompleteDefeat = () => { };

        private void OnStartAttack()
        {
            OnBubbleStartAttack.Invoke();
            //print("Attack started");
        }

        private void OnCompleteAttack()
        {
            OnBubbleCompleteAttack.Invoke();
            //print("Attack completed");
        }

        private void OnHit()
        {
            OnBubbleHit.Invoke();
        }

        private void OnCompleteDefeat()
        {
            OnBubbleCompleteDefeat.Invoke();
        }
    }
}

// the Invoke method is available on all events
// Events must have values before calling the INVOKE method!!!
// Empty Lambda Expressions --- event = () => { }
// Lambda Expressions as aninymous function for quickly prototyping a behaviour, writing a lambda expression is shorter than writing a regular function
// Event keyword prevents an external class from overriding the list of registered methods in the event