using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    public string keyword;
    [TextArea]
    public string description;

    public abstract void RespondToInput(GameController controller, string[] arguments);
}
