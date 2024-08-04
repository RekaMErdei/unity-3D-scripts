using System;
using UnityEngine;
using RPG.Core;
using RPG.Quest;
using RPG.Utility;

namespace RPG.Character
{
    public class PlayerController : MonoBehaviour
    {
        public CharacterStatsSO stats;
        private Health healthCmp;
        private Combat combatCmp;
        private GameObject axeWeapon;
        private GameObject swordWeapon;

        public Weapons weapon = Weapons.Axe;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats.");
            }

            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            axeWeapon = GameObject.FindGameObjectWithTag(Constants.AXE_TAG);
            swordWeapon = GameObject.FindGameObjectWithTag(Constants.SWORD_TAG);
        }

        private void OnEnable()
        {
            EventManager.OnReward += HandleReward;
        }

        private void OnDisable()
        {
            EventManager.OnReward -= HandleReward;
        }

        private void Start()
        {
            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            SetWeapon();
        }

        private void HandleReward(RewardSO reward)
        {
            healthCmp.healthPoints += reward.bonusHealth;
            healthCmp.potionCount += reward.bonusPotions;
            combatCmp.damage += reward.bonusDamage;

            EventManager.RaiseChangePlayerHealth(healthCmp.healthPoints);
            EventManager.RaiseChangePlayerPotions(healthCmp.potionCount);

            if (reward.forceWeaponSwap)
            {
                weapon = reward.weapon;
                SetWeapon();
            }

            print("Reward received");
        }

        private void SetWeapon()
        {
            if (weapon == Weapons.Axe)
            {
                axeWeapon.SetActive(true);
                swordWeapon.SetActive(false);
            }
            else
            {
                axeWeapon.SetActive(false);
                swordWeapon.SetActive(true);
            }
        }
    }
}


// add RPG.Core namespace and EventManager.method... for connect Player to UI