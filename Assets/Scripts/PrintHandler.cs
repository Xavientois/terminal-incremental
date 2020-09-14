using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintHandler : MonoBehaviour
{
    private const uint CommandMax = 5000;

    public Text displayText;
    [HideInInspector] public InputBlockerHandler inputBlockerHandler;

    private Queue<string> commandLog = new Queue<string>();
    private Queue<Tuple<string, float>> printQueue = new Queue<Tuple<string, float>>();

    private void Awake()
    {
        inputBlockerHandler = GetComponent<InputBlockerHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayLoggedCommands();
    }

    private void DisplayLoggedCommands()
    {
        string logAsText = string.Join("", commandLog.ToArray());

        displayText.text = logAsText;
    }

    public void DelayedPrint(float delaySeconds, string commandToAdd)
    {
        printQueue.Enqueue(new Tuple<string, float>(commandToAdd, delaySeconds));
    }

    // Update is called once per frame
    void Update()
    {
        float totalDelay = 0;

        while (printQueue.Count > 0)
        {
            var printJob = printQueue.Dequeue();
            float delaySeconds = printJob.Item2;
            string commandToAdd = printJob.Item1;

            totalDelay += delaySeconds;
            inputBlockerHandler.AddInputBlocker();

            WaitHelper.WaitSeconds(totalDelay, () => {
                commandLog.Enqueue(commandToAdd);
                DisplayLoggedCommands();
                inputBlockerHandler.RemoveInputBlocker();
            });
        }
        

        while (commandLog.Count > CommandMax)
        {
            commandLog.Dequeue();
        }
    }
}
