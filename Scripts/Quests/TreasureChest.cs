using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;

namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private QuestItemSO questItem;
        public Animator animatorCmp;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;

        private void OnTriggerEnter()
        {
            isInteractable = true;
            // print("Player detected!");
        }

        private void OnTriggerExit()
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened || !context.performed) return;

            EventManager.RaiseTreasureChestUnlocked(questItem);

            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            // print("Opening treasure chest");
            hasBeenOpened = true;
        }

    }
}

// if (... !context.performed) -- need to check the perfomred action otherwise this method will be called multiple times
//  means without !context.performed you can pick up the candy more times, not just once
/*
        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened || !context.performed) return;

            EventManager.RaiseTreasureChestUnlocked(questItem, true);

            animatorCmp.SetBool(
                Constants.IS_SHAKING_ANIMATOR_PARAM, false
            );
            hasBeenOpened = true;
        }
*/
