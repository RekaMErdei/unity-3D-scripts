using UnityEngine;
using RPG.Core;

namespace RPG.Quest
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] private RewardSO reward;

        private bool rewardTaken = false;

        public void SendReward()
        {
            if (rewardTaken) return;

            rewardTaken = true;
            EventManager.RaiseReward(reward);
        }
    }
}

// using RPG.Core; -- to access the event manager file
