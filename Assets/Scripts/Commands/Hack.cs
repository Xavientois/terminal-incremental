using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "HackerGame/InputActions/Hack")]
public class Hack : Command
{
    public AudioClip hackingSound;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        Machine.Type machineType = Machine.Type.PC;

        if (arguments.Length == 1)
        {
            controller.PrintLn("Hacking...");
            controller.audioSource.PlayOneShot(hackingSound);
            controller.LoadAction(controller.currentUser.machineHackTime[(int)machineType], () =>
            {
                controller.audioSource.Stop();
                controller.AddMachines(machineType, 1);
                controller.PrintLn(Machine.Name(machineType) + " hacked!\n");
            });
        }
        else
        {
            controller.DelayedPrintLn(1, "Hacking failed...");
        }
    }

}
