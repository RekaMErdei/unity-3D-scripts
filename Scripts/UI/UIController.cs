using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Quest;
//using System;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentCmp;
        public VisualElement root;
        public List<Button> buttons = new List<Button>();
        public VisualElement mainMenuContainer;
        public VisualElement playerInfoContainer;
        public UIBaseState currentState;
        public UIMainMenuState mainMenuState;
        public UIDialogueState dialogueState;
        public UIQuestItemState questItemState;

        public Label healthLabel;
        public Label potionsLabel;
        private VisualElement questItemIcon;

        public int currentSelection = 0;

        private void Awake()
        {
            uiDocumentCmp = GetComponent<UIDocument>();
            root = uiDocumentCmp.rootVisualElement;

            mainMenuContainer = root.Q<VisualElement>("main-menu-container");
            playerInfoContainer = root.Q<VisualElement>("player-info-container");
            healthLabel = playerInfoContainer.Q<Label>("health-label");
            potionsLabel = playerInfoContainer.Q<Label>("potions-label");
            questItemIcon = playerInfoContainer.Q<VisualElement>("quest-item-icon");

            mainMenuState = new UIMainMenuState(this);
            dialogueState = new UIDialogueState(this);
            questItemState = new UIQuestItemState(this);
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions += HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue += HandleInitiateDialogue;
            EventManager.OnTreasureChestUnlocked += HandleTreasureChestUnlocked;
        }

        // Start method is called before the first frame update
        void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 0)
            {
                currentState = mainMenuState;
                currentState.EnterState();
            }
            else
            {
                playerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions -= HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
            EventManager.OnTreasureChestUnlocked -= HandleTreasureChestUnlocked;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            currentState.SelectButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || buttons.Count == 0) return;

            buttons[currentSelection].RemoveFromClassList("active");

            Vector2 input = context.ReadValue<Vector2>();
            currentSelection += input.x > 0 ? 1 : -1;

            currentSelection = Mathf.Clamp(
                currentSelection, 0, buttons.Count - 1
                );

            buttons[currentSelection].AddToClassList("active");

            print(currentSelection);
        }

        private void HandleChangePlayerHealth(float newHealthPoints)
        {
            healthLabel.text = newHealthPoints.ToString();
        }

        private void HandleChangePlayerPotions(int newPotionCount)
        {
            potionsLabel.text = newPotionCount.ToString();
        }

        private void HandleInitiateDialogue(TextAsset inkJSON, GameObject npc)
        {
            currentState = dialogueState;
            currentState.EnterState();

            (currentState as UIDialogueState).SetStory(inkJSON, npc);
        }

        private void HandleTreasureChestUnlocked(QuestItemSO item)
        {
            currentState = questItemState;
            currentState.EnterState();

            (currentState as UIQuestItemState).SetQuestItemLabel(item.itemName);

            questItemIcon.style.display = DisplayStyle.Flex;
        }

    }
}

// just keep code clean prefered to set up the variables within Awake method and logic in the Start method
// VisualElement class is responsible for managing elements created from the visual element
// Unity creates an event from each action (based on Game controls Input action settings)
// public void HandleInteract(InputAction.CallbackContext context) -- Define a method to handling this event
// buttons.Count == 0 -- it is possible if the menu does not have buttons
// ? 1 : -1; -- Ternary operator equals with IF statement (? = if, : = else)
/* currentSelection += (int)input.x;
if (input.x > 0)
{
    currentSelection += 1;
}
else
{
    currentSelection -= 1;
}
*/
// Q.() -- Query methods selects the fist element that find - use the Q method wen you want to select a single element
// root.Q<VisualElement>("main-menu-container") -- visaul element class after name of the element
// int sceneIndex = SceneManager.GetActiveScene().buildIndex; -- define a new variables(or element)=class.method().property;
// Label class -- has a property for modifying the inner text of the element (Visual element class doesn't support this feature)
// (xy as yz) -- the as keyword allows developers to change the type of the variable, typecasting only applies to this line of code
// = new List<Button>(); -- BUG fixing

// Right order to connect two separate game object
//  1. NPCController component is responsible for raising the event
//  2. The EventManager class will define the event
//  3. Lastly the UIController component handles the event