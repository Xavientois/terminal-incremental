using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHandler : MonoBehaviour
{
    [HideInInspector] public PrintHandler printHandler;

    public InputField inputField;

    public string BlockString = "█";

    void Awake()
    {
        printHandler = GetComponent<PrintHandler>();
    }

    // Load an action and perform it once loading is complete
    public void BlockLoadAction(float seconds, int blocks, Action action)
    {
        BlockLoad(seconds, blocks);
        WaitHelper.WaitSeconds(seconds + 0.1f, action);
    }

    public void BlockLoad(float seconds, int blocks)
    {
        float timeBetweenBlocks = seconds / blocks;

        for (int i = 1; i < blocks + 1; i++)
        {
            printHandler.DelayedPrint(timeBetweenBlocks, BlockString);
        }
        printHandler.DelayedPrint(0, "\n\n");
    }
}
