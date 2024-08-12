using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(
        fileName = "Character Stats",
        menuName = "RPG/Character Stats SO",
        order = 0
    )]

    public class CharacterStatsSO : ScriptableObject
    {
        public float health = 100f;
        public float damage = 10f;
        public float walkSpeed = 1f;
        public float runSpeed = 1.5f;
    }
}

// menuName = "RPG/Character Stats SO", -- RPG/ create a submenu element in Unity, so you can create folder in Unity menu
// right click - Create - RPG - Character Stats SO
