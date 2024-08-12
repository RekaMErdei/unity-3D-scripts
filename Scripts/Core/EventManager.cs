using UnityEngine;
using UnityEngine.Events;
using RPG.Quest;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePlayerPotions;
        public static event UnityAction<TextAsset, GameObject> OnInitiateDialogue;
        public static event UnityAction<QuestItemSO> OnTreasureChestUnlocked;
        public static event UnityAction<bool> OnToggleUI;
        public static event UnityAction<RewardSO> OnReward;
        public static event UnityAction<Collider, int> OnPortalEnter;

        public static void RaiseChangePlayerHealth(float newHealthPoints) =>
            OnChangePlayerHealth?.Invoke(newHealthPoints);

        public static void RaiseChangePlayerPotions(int newHealthPotions) =>
            OnChangePlayerPotions?.Invoke(newHealthPotions);

        public static void RaiseInitiateDialogue(TextAsset inkJSON, GameObject npc) =>
            OnInitiateDialogue?.Invoke(inkJSON, npc);

        public static void RaiseTreasureChestUnlocked(QuestItemSO item) =>
            OnTreasureChestUnlocked?.Invoke(item);

        public static void RaiseToggleUI(bool isOpened) =>
            OnToggleUI?.Invoke(isOpened);

        public static void RaiseReward(RewardSO reward) =>
            OnReward?.Invoke(reward);

        public static void RaisePortalEnter(Collider player, int nextSceneIndex) =>
            OnPortalEnter?.Invoke(player, nextSceneIndex);

    }
}

// UnityAction OnChangePlayerHealth; -- this event will be aimed at helping bridge the UI and the Player
// => -- shorthand syntax for defining a method (equals {method})
// ? -- redered the Null Conditional Operator (!= null), use it just in case you DON'T use MonoBehaviour class
//  OnChangePlayerHealth?.Invoke(); -- Before the method is accesssed, C# will check the value on the left for the value...
//  ...If the variable is empty, C# won't execute the code on the right