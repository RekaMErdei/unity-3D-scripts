
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Ink.Runtime;
using RPG.Utility;
using RPG.Character;
using RPG.Quest;
using UnityEditor.Search;
using System.ComponentModel;
//using Ink.Parsed;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private Story currentStory;
        private PlayerInput playerInputCmp;
        private NPCController npcControllerCmp;

        private bool hasChoices = false;

        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = controller.root.Q<VisualElement>("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;

            playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAME_MANAGER_TAG
            ).GetComponent<PlayerInput>();
            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);

        }

        public override void SelectButton()
        {
            UpdateDialogue();
        }

        public void SetStory(TextAsset inkJSON, GameObject npc)
        {
            currentStory = new Story(inkJSON.text);
            currentStory.BindExternalFunction("VerifyQuest", VerifyQuest);

            npcControllerCmp = npc.GetComponent<NPCController>();

            if (npcControllerCmp.hasQuestItem)
            {
                currentStory.ChoosePathString("postCompletion");
            }

            UpdateDialogue();
        }

        public void UpdateDialogue()
        {
            if (hasChoices)
            {
                currentStory.ChooseChoiceIndex(controller.currentSelection);
            }

            if (!currentStory.canContinue)
            {
                ExitDialogue();
                return;
            }

            dialogueText.text = currentStory.Continue();

            hasChoices = currentStory.currentChoices.Count > 0;
            if (hasChoices)
            {
                HandleNewChoices(currentStory.currentChoices);
            }
            else
            {
                nextButton.style.display = DisplayStyle.Flex;
                choicesGroup.style.display = DisplayStyle.None;
            }
        }

        private void HandleNewChoices(List<Choice> choices)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            controller.buttons?.Clear();

            choices.ForEach(CreateNewChoiceButton);

            controller.buttons = choicesGroup.Query<Button>().ToList();
            controller.buttons[0].AddToClassList("active");

            controller.currentSelection = 0;
        }

        private void CreateNewChoiceButton(Choice choice)
        {
            Button choiceButton = new Button();

            choiceButton.AddToClassList("menu-button");
            choiceButton.text = choice.text;
            choiceButton.style.marginRight = 20;

            choicesGroup.Add(choiceButton);
        }

        private void ExitDialogue()
        {
            dialogueContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
        }

        public void VerifyQuest()
        {
            currentStory.variablesState["questCompleted"] = npcControllerCmp.CheckPlayerForQuestItem();
            //Debug.Log("Verifying Quest");
        }

    }
}

// class Ink.Runtime.Story -- Story is the core class that represents a complete in narrative and manages the evaluation and the state of it.
// Continue() -- continue the story for one line of the content, if possible
// RPG.Utility -- namespace for handling constants
// playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP); -- this should effectively stop the player from moving
// private void CreateNewChoiceButton(Choice choice) -- with this method UI defined buttons can appear within Gameplay mode
// currentStory.ChooseChoiceIndex(controller.currentSelection); -- allow to select a choice and move on the next dialogue
// controller.currentSelection =0; -- BUG FIXING!!!
// variablesState object within the story -- the state of the variables in the ink engine
// ChoosePathString("case sensitive string!!!") -- all stories have this method

// SHIFT-Alt-A combo -- comment in and out /* */