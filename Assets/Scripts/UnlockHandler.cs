using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A condition for unlocking a specific command based on collecting a certain number of
/// a certain type of machine
/// </summary>
[System.Serializable]
public class MilestoneCondition
{
    public Machine.Type machineType;
    public uint count;

    public List<string> unlockedCommands;
    public List<string> unlockedTransitions;

    bool completed = false;

    public void MarkCompleted()
    {
        completed = true;
    }

    public bool IsCompleted()
    {
        return completed;
    }

    public bool HitsMilestone(User user)
    {
        return user.machineCounts.Length > (int)machineType
            && user.machineCounts[(int)machineType] >= count;
    }

    public string UnlockText()
    {
        return (unlockedCommands.Count > 0 ? "New commands unlocked:\n" + string.Join("\n", unlockedCommands) + "\n\n" : "")
            + (unlockedTransitions.Count > 0 ? "New menus unlocked:\n" + string.Join("\n", unlockedTransitions) + "\n\n" : ""); ;
    }
}

public class UnlockHandler : MonoBehaviour
{
    public List<MilestoneCondition> milestoneUnlocks;

    private GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    private void Update()
    {
        foreach (MilestoneCondition milestone in milestoneUnlocks)
        {
            if (!milestone.IsCompleted()
                && controller.currentUser != null
                && milestone.HitsMilestone(controller.currentUser))
            {
                controller.currentUser.unlockedCommands.UnionWith(milestone.unlockedCommands);
                controller.currentUser.unlockedTransitions.UnionWith(milestone.unlockedTransitions);

                controller.PrintLn(milestone.UnlockText());

                milestone.MarkCompleted();
            }
        }
    }
}
