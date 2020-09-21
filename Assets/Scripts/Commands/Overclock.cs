using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Overclock")]
public class Overclock : Command
{
    public GameConfig config;

    public AudioClip overclockingSound;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        Machine.Type machineType = Machine.Type.PC;

        if (arguments.Length < 3)
        {
            uint machinesToSpend;
            if (arguments.Length == 2)                
            {
                if (!UInt32.TryParse(arguments[1], out machinesToSpend))
                {
                    controller.PrintLn("Error: cannot parse number of " + Machine.Name(machineType) + "s to use");
                    return;
                }
            }
            else
            {
                machinesToSpend = 1;
            }

            bool spendSuccess = controller.SpendMachines(machineType, machinesToSpend);

            if (spendSuccess)
            {
                controller.PrintLn("Current hacking speed for " + Machine.Name(machineType) + "s: " + controller.currentUser.machineHackTime[(int)machineType] + " seconds\n");

                controller.PrintLn("Corrupting " + machinesToSpend + " " + Machine.Name(machineType)
                    + "s to overclock hacking processor...");
                controller.audioSource.PlayOneShot(overclockingSound);
                controller.LoadAction(
                    controller.currentUser.machineHackTime[(int)machineType] * config.overclockingMultiplier,
                    () =>
                    {
                        controller.audioSource.Stop();

                        controller.currentUser.machineHackTime[(int)machineType] *= Mathf.Pow(1 - config.overclockingDecrease, machinesToSpend);

                        controller.PrintLn(Machine.Name(machineType) + " hacking overclocked! " + Machine.Name(machineType) + "s can now be hacked in " +
                            controller.currentUser.machineHackTime[(int)machineType] + " seconds!\n");
                    });
            }
            else
            {
                controller.PrintLn("Error: Requested more " + Machine.Name(machineType) + "s than are available");
            }
        }
        else
        {
            controller.DelayedPrintLn(0.5f, "Error: Recieved too many arguments.\n" +
                "See `help " + keyword + "` for more details.");
        }
    }
}
