using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Logout")]
public class Logout : Command
{
    public Menu logoutMenu;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        if (arguments.Length == 1)
        {

            controller.Print("Logging out.");
            controller.Logout();
            controller.DelayedPrint(1, ".");
            controller.DelayedPrint(1, ".");
            controller.DelayedPrintLn(0.5f, "\n");
            controller.menuNavigation.TransitionToMenu(logoutMenu);
        }
        else
        {
            controller.PrintLn("ERROR: Too many arguments");
        }
    }
}
