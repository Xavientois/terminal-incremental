using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandHistory : MonoBehaviour
{
    [HideInInspector] public InputField inputField;
    [HideInInspector] public TextInput textInput;
    [HideInInspector] public GameController controller;

    private List<string> history = new List<string>();
    private uint history_index = 0;

    private void Awake()
    {
        controller = GetComponent<GameController>();
        textInput = GetComponent<TextInput>();
        inputField = textInput.inputField;
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = HistoryIndexChange();

        if (changed
            && textInput.active
            && controller.IsHistoryEnabled()
            && inputField.contentType == InputField.ContentType.Standard)
        {
            if (history_index > 0)
            {
                int access_index = (int)(history.Count - history_index);

                inputField.text = history[access_index];
            }
            else
            {
                inputField.text = "";
            }
        }               
    }

    bool HistoryIndexChange()
    {
        bool changed = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && history_index < history.Count)
        {
            ++history_index;
            changed = true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && history_index > 0)
        {
            --history_index;
            changed = true;
        }

        return changed;
    }

    public void Add(string command)
    {
        history.Add(command);
    }

    public void ResetIndex()
    {
        history_index = 0;
    }

    public void Clear()
    {
        history.Clear();
    }
}
