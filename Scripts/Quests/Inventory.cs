using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Quest
{
    public class Inventory : MonoBehaviour
    {
        public List<QuestItemSO> items = new List<QuestItemSO>();

        private void OnEnable()
        {
            EventManager.OnTreasureChestUnlocked += HandleTreasureChestUnlocked;
        }

        private void OnDisable()
        {
            EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;
        }

        public void HandleTreasureChestUnlocked(QuestItemSO newItem)
        {
            items.Add(newItem);
        }

        public bool HasItem(QuestItemSO desiredItem)
        {
            bool itemFound = false;

            items.ForEach((QuestItemSO item) =>
            {
                if (desiredItem.name == item.name) itemFound = true;
            });

            return itemFound;
        }

    }
}


// using System.Collections.Generic; -- could handle lists not just arrays
//  Initially the player hasn't item inthe inventroy. 
//  Add new items is not possible with arrays, easyer to add items to lists