using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float damage = 0f;
        [NonSerialized] public bool isAttacking = false;

        private Animator animatorCmp;
        private BubbleEvent bubbleEventCmp;
        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();

        }

        private void OnEnable()
        {
            bubbleEventCmp.OnBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack += HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit += HandleBubbleHit;
        }

        private void OnDisable()
        {
            bubbleEventCmp.OnBubbleStartAttack -= HandleBubbleStartAttack;
            bubbleEventCmp.OnBubbleCompleteAttack -= HandleBubbleCompleteAttack;
            bubbleEventCmp.OnBubbleHit -= HandleBubbleHit;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (isAttacking) return;

            animatorCmp.SetFloat(Constants.SPEED_ANIMATOR_PARAM, 0);
            animatorCmp.SetTrigger(Constants.ATTACK_ANIMATION_PARAM);
        }

        private void HandleBubbleStartAttack()
        {
            isAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            isAttacking = false;
        }

        private void HandleBubbleHit()
        {
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward,
                transform.localScale / 2,
                transform.forward,
                transform.rotation,
                1f
            );

            foreach (RaycastHit target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;

                Health healthCmp = target.transform.gameObject.GetComponent<Health>();

                if (healthCmp == null) continue;

                healthCmp.TakeDamage(damage);

                print(target.transform.name);
            }

            //print("Hit registered");
        }

        public void CancelAttack()
        {
            animatorCmp.ResetTrigger(Constants.ATTACK_ANIMATION_PARAM);
        }
    }
}

// RaycastHit[] --- the pair of square brackets means that the return value (targets) is an array!!!
// foreach keyword allows us to loop through an array
