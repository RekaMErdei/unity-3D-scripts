namespace RPG.UI
{
    public abstract class UIBaseState
    {
        public UIController controller;

        public UIBaseState(UIController ui)
        {
            controller = ui;
        }

        public abstract void EnterState();

        public abstract void SelectButton();
    }
}

// abstract class allows you to force a class to inherit an attribute
// Whanever a new state is created, we must call this constructor method. Otherwise, none of our state will have access to the istance.
//         public UIBaseState(UIController ui){controller = ui;}  -- this is the constructor method
// create Istance -- accept an instance of the UIController class as a parameter calledc UI -- UIBaseState(UIController ui)