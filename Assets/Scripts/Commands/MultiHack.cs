using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MultiHackCost
{
    public string keyword;

    [Header("Cost")]
    public Machine.Type machineCostType;
    public uint costCount;

    [Header("Result")]
    public Machine.Type machineResultType;
    public uint resultCount = 1;

    public string MultiHackText()
    {
        return (costCount > 0 ? "Corrupted " + costCount + " " + Machine.Name(machineCostType) + "s\n" : "")
            + "Hacked " + resultCount + " " + Machine.Name(machineResultType) + "s\n";
    }
}

[CreateAssetMenu(menuName = "HackerGame/InputActions/MultiHack")]
public class MultiHack : Command
{
    public List<MultiHackCost> hackCosts;

    public override void RespondToInput(GameController controller, string[] arguments)
    {
        if (arguments.Length == 1)
        {
            controller.PrintLn("Error: Missing target of multi-hack\nExpected format: " + keyword + " [target]\n");
            controller.PrintLn("Available targets:");
            controller.PrintLn(AvailableTargets(controller.currentUser));
        }
        else if (arguments.Length == 2)
        {
            foreach (var cost in hackCosts)
            {
                if (arguments[1] == cost.keyword)
                {
                    controller.PrintLn("Hacking " + Machine.Name(cost.machineResultType) + "...");
                    controller.SpendMachines(cost.machineCostType, cost.costCount);
                    controller.LoadAction(controller.currentUser.machineHackTime[(int)cost.machineResultType], () =>
                    {
                        controller.AddMachines(cost.machineResultType, cost.resultCount);
                        controller.PrintLn(cost.MultiHackText());
                    });
                }
            }
        }
        else
        {
            controller.DelayedPrintLn(1, "Error: Too many arguments");
        }
    }

    private string AvailableTargets(User user)
    {
       return string.Join("\n",
           hackCosts
                .FindAll(cost => user.machineCounts[(int)cost.machineCostType] >= cost.costCount)
                .ConvertAll(cost => cost.keyword)
           );
    }

}
