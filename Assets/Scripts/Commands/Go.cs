using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Go")]
public class Go : Command
{
    public override void RespondToInput(GameController controller, string[] arguments)
    {
        if (arguments.Length > 1)
        {
            controller.menuNavigation.AttemptToTransitionMenu(arguments[1]);
        }
        else
        {
            controller.PrintLn("ERROR: Missing menu name!");
        }
    }
}
