using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Help")]
public class Help : Command
{
    public override void RespondToInput(GameController controller, string[] arguments)
    {
        if (arguments.Length == 1)
        {
            controller.PrintLn("\nAvailable Commands:");
            foreach (var command in controller.GetAvailableCommands())
            {
                if (command.keyword != "" && controller.currentUser.unlockedCommands.Contains(command.keyword))
                {
                    controller.PrintLn(command.keyword);
                }
            }
            controller.PrintLn("\nUse 'help [command]' to get details on a specific command");
        }
        else if (arguments.Length > 1)
        {
            for (int i = 1; i < arguments.Length; i++)
            {
                Command command = Array.Find(controller.GetAvailableCommands(), c => c.keyword == arguments[i]);
                if (command && command.description != "" && controller.currentUser.unlockedCommands.Contains(command.keyword))
                {
                    controller.PrintLn(command.keyword + ": " + command.description);
                }
            }
        }
    }
}
