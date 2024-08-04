using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
using UnityEngine.Events;
using RPG.Core;
using UnityEngine.UI;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };
        [NonSerialized] public float healthPoints = 0f;
        public int potionCount = 1;
        [SerializeField] private float healAmount = 15f;
        [NonSerialized] public Slider sliderCmp;

        private Animator animatorCmp;
        private BubbleEvent bubbleEventCmp;
        private bool isDefeated = false;

        private void Awake()
        {
            animatorCmp = GetComponentInChildren<Animator>();
            bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
            sliderCmp = GetComponentInChildren<Slider>();
        }

        private void OnEnable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        public void Start()
        {
            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerPotions(potionCount);
            }
        }

        private void OnDisable()
        {
            bubbleEventCmp.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        public void TakeDamage(float damageAmount)
        {
            healthPoints = Mathf.Max(healthPoints - damageAmount, 0);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerHealth(healthPoints);
            }

            if (sliderCmp != null)
            {
                sliderCmp.value = healthPoints;
            }

            if (healthPoints == 0)
            {
                Defeated();
            }

            //print(healthPoints);
        }

        private void Defeated()
        {
            if (isDefeated) return;
            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated.Invoke();
            }

            isDefeated = true;

            animatorCmp.SetTrigger(Constants.DEFEATED_ANIMATOR_PARAM);
        }

        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0) return;
            potionCount--;
            healthPoints += healAmount;

            EventManager.RaiseChangePlayerHealth(healthPoints);
            EventManager.RaiseChangePlayerPotions(potionCount);
        }
    }
}

// add RPG.Core namespace and EventManager.method... for connect Player to UI
// 'potionCount--;' decrement operator -- equals with potionCount = potionCount -1;