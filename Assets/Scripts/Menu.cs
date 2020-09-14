using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HackerGame/Menu")]
public class Menu : ScriptableObject
{
    [TextArea]
    public string description;
    public string title;
    public MenuTransition[] transitions;
    public Command[] availableCommands;

    public bool historyEnabled = true;
    public bool echoInput = true;
    public bool shouldSplitInput = true;
}
