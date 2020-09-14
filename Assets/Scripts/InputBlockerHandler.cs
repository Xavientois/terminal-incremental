using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InputBlockerHandler : MonoBehaviour
{
    [HideInInspector] public InputField inputField;
    [HideInInspector] public TextInput textInput;

    private uint activePrintJobs = 0;

    private void Awake()
    {
        textInput = GetComponent<TextInput>();
        inputField = textInput.inputField;
    }

    public void AddInputBlocker()
    {
        activePrintJobs += 1;

        inputField.readOnly = true;
        textInput.active = false;
        inputField.transform.localScale = new Vector3(2, 1, 1);
    }

    public void RemoveInputBlocker()
    {
        --activePrintJobs;
        if (activePrintJobs == 0)
        {
            inputField.readOnly = false;
            textInput.active = true;
            inputField.transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

    public void Update()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}
