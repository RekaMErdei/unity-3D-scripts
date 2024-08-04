using UnityEngine;
using UnityEngine.UIElements;
using RPG.Core;
// using Unity.VisualScripting;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
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
                SceneTransition.Initiate(1);
            }

            //Debug.Log(btn.name);
        }

    }
}

// .ToList(); -- converting the results to a list
// SceneTransition.Initiate(1); much better than SceneManager.LoadScene(1); 
//  because the SceneTransition can manage more settings parallel e.g. audio