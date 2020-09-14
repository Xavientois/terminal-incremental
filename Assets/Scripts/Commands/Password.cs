using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Password")]
public class Password : Command
{
    public Menu mainMenu;
    public Menu loginMenu;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        SetToStandard(controller.inputField);

        string username = controller.currentUsername;
        string password = arguments[0];

        PrintPassword(controller, password);

        if (controller.users.ContainsKey(username))
        {
            if (controller.users[username].password == password)
            {
                controller.Print("Logging in.");
                controller.DelayedPrint(0.5f, ".");
                controller.DelayedPrint(1, ".");
                controller.DelayedPrintLn(0.5f, "\n\nHello, " + username);
                WaitHelper.WaitSeconds(2, () =>
                {
                    controller.Login(username);
                });
                controller.menuNavigation.TransitionToMenu(mainMenu);
            }
            else
            {
                controller.DelayedPrintLn(1, "Password incorrect for user: " + username);
                controller.menuNavigation.TransitionToMenu(loginMenu);
            }

        }
        else
        {
            User newUser = new User(username, password, controller.config);
            controller.users.Add(username, newUser);
            controller.Login(username);

            controller.DelayedPrintLn(2, "Hello, " + username);
            controller.menuNavigation.TransitionToMenu(mainMenu);
        }
    }

    private void PrintPassword(GameController controller, string password)
    {
        char passwordMask = controller.inputField.asteriskChar;
        int count = password.Length;

        controller.PrintLn(new string(passwordMask, count));
        controller.PrintLn();
    }

    public void SetToStandard(InputField inputField)
    {
        inputField.text = "";
        inputField.contentType = InputField.ContentType.Standard;
        inputField.DeactivateInputField();
        inputField.ActivateInputField();
    }

    private void LogAccounts(GameController controller)
    {
        foreach (KeyValuePair<string, User> kvp in controller.users)
            Debug.Log("Key = " + kvp.Key + ", Value = " + kvp.Value.password);
    }

}