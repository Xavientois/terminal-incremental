using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Username")]
public class Username : Command
{
    public Menu passwordMenu;

    public bool notifyForNewAccount = false;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        if (arguments.Length == 1)
        {
            controller.currentUsername = arguments[0];

            if (notifyForNewAccount && !controller.users.ContainsKey(arguments[0]))
            {
                controller.PrintLn("Creating new user account. Please enter a password for account: " + arguments[0] + "\n");
            }

            SetToPassword(controller.inputField);
            controller.menuNavigation.TransitionToMenu(passwordMenu);
        }
        else
        {
            controller.PrintLn("Usernames cannot contain spaces.");
        }
    }


    public void SetToPassword(InputField inputField)
    {
        inputField.text = "";
        inputField.contentType = InputField.ContentType.Password;
        inputField.DeactivateInputField();
        inputField.ActivateInputField();
    }

}
