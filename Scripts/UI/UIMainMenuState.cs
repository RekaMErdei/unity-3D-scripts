using UnityEngine;
using UnityEngine.UIElements;
using RPG.Core;
// using Unity.VisualScripting;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        private int sceneIndex;
        public UIMainMenuState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            if (PlayerPrefs.HasKey("SceneIndex"))
            {
                sceneIndex = PlayerPrefs.GetInt("SceneIndex");
                AddButton();
            }

            controller.mainMenuContainer.style.display = DisplayStyle.Flex;

            controller.buttons = controller.mainMenuContainer.Query<Button>(null, "menu-button").ToList();

            controller.buttons[0].AddToClassList("active");

            //Debug.Log(controller.buttons.Count);
        }

        public override void SelectButton()
        {
            Button btn = controller.buttons[controller.currentSelection];

            if (btn.name == "start-button")
            {
                PlayerPrefs.DeleteAll();
                SceneTransition.Initiate(1);
            }
            else
            {
                SceneTransition.Initiate(sceneIndex);
            }

            //Debug.Log(btn.name);
        }

        private void AddButton()
        {
            Button continueButton = new Button();

            continueButton.AddToClassList("menu-button");
            continueButton.text = "Continue";

            VisualElement mainMenuButtons = controller.mainMenuContainer.Q<VisualElement>("buttons");
            mainMenuButtons.Add(continueButton);
        }

    }
}

// .ToList(); -- converting the results to a list
// SceneTransition.Initiate(1); much better than SceneManager.LoadScene(1); 
//  because the SceneTransition can manage more settings parallel e.g. audio
// PlayerPrefs.DeleteKey(); -- this method deletes a single piece of data
// PlayerPrefs.DeleteAll(); -- this method deletes every data that have stored and saved