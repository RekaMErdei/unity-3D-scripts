using UnityEngine;
using RPG.Character;

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnPortalEnter += HandlePortalEnter;
        }

        private void OnDisable()
        {
            EventManager.OnPortalEnter -= HandlePortalEnter;
        }

        private void HandlePortalEnter(Collider player, int nextSceneIndex)
        {
            PlayerController playerControllerCmp = player.GetComponent<PlayerController>();

            PlayerPrefs.SetFloat("Health", playerControllerCmp.healthCmp.healthPoints);
            PlayerPrefs.SetInt("Potions", playerControllerCmp.healthCmp.potionCount);
            PlayerPrefs.SetFloat("Damage", playerControllerCmp.combatCmp.damage);
            PlayerPrefs.SetInt("Weapon", (int)playerControllerCmp.weapon);
            PlayerPrefs.SetInt("SceneIndex", nextSceneIndex);
        }


    }
}

// OnEnable and OnDisable method are necessary for registering the events
// PlayerPrefs -- is a class that stores Player preferences between game sessions. 
//  It can store string, float and integer values into the user’s platform registry.
// SetFloat() -- for saving values with the float type. Sets the float value of the preference identified by the given key. 
// GetFloat() -- You can use PlayerPrefs.GetFloat to retrieve this value.
// SetInt -- save integer data
// (int)playerControllerCmp.weapon -- int is necessary because the weapon type is enum and PlayerPrefs does not support enums!