using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        public TextAsset inkJSON;
        public QuestItemSO desiredQuestItem;
        private Canvas canvasCmp;
        private Reward rewardCmp;
        public bool hasQuestItem = false;

        private void Awake()
        {
            canvasCmp = GetComponentInChildren<Canvas>();
            rewardCmp = GetComponent<Reward>();
        }

        private void OnTriggerEnter()
        {
            canvasCmp.enabled = true;
        }

        private void OnTriggerExit()
        {
            canvasCmp.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasCmp.enabled) return;
            if (inkJSON == null)
            {
                Debug.LogWarning("Please add an ink file to the npc!");
                return;
            }

            EventManager.RaiseInitiateDialogue(inkJSON, gameObject);
        }

        public bool CheckPlayerForQuestItem()
        {
            if (hasQuestItem) return true;

            Inventory inventoryCmp = GameObject.FindGameObjectWithTag(
                Constants.PLAYER_TAG
            ).GetComponent<Inventory>();

            hasQuestItem = inventoryCmp.HasItem(desiredQuestItem);

            if (rewardCmp != null && hasQuestItem)
            {
                rewardCmp.SendReward();
            }

            return hasQuestItem;
        }

    }
}

// OnTriggerEnter() -- this method is executed by the Collider component when an object enters the Collider
// .enabled -- this property can enable or disable a component
// public QuestItemSO desiredQuestItem; -- in Unity you can select the dsired quest item in NPCController component

// Right order to connect two separate game object
//  1. NPCController component is responsible for raising the event
//  2. The EventManager class will define the event
//  3. Lastly the UIController component handles the event