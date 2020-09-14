using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    private GameController controller;
    private CommandHistory history;

    public InputField inputField;

    public AudioClip enterSound;

    public bool active = true;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        history = GetComponent<CommandHistory>();

        inputField.onEndEdit.AddListener(AcceptStringInput);

        inputField.text = "";
    }

    void AcceptStringInput(string userInput)
    {
        if (!active)
        {
            return;
        }

        controller.audioSource.PlayOneShot(enterSound);

        userInput = userInput.ToLower();

        if (controller.ShouldEchoInput())
        {
            controller.PrintLn(userInput + "\n");
        }

        if (userInput.Length > 0)
        {
            if (controller.IsHistoryEnabled())
            {
                history.Add(userInput);
                history.ResetIndex();
            }

            char[] delimiterCharacters = { ' ' };
            string[] arguments = controller.ShouldSplitInput()
                ? userInput.Split(delimiterCharacters, System.StringSplitOptions.RemoveEmptyEntries)
                : new string[] { userInput };

            bool handled = controller.HandleCommand(arguments);

            if (!handled)
            {
                controller.PrintLn("Unrecognized command: " + arguments[0]);
                controller.PrintLn("For a list of available commands, type: help");
            }
        }

        InputComplete();
    }

    void InputComplete()
    {
        inputField.ActivateInputField();
        inputField.text = null;
    }
}
