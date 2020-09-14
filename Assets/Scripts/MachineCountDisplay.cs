using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineCountDisplay : MonoBehaviour
{
    public User user;
    public Text textDisplay;

    public Machine.Type machineType;

    private void Awake()
    {
        textDisplay = GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateText()
    {
        Debug.Assert(user != null);
        Debug.Assert(textDisplay != null);

        textDisplay.text = Machine.ShortName(machineType) + "s:\n"
                         + user.machineCounts[(int)machineType]
                         + "\n═══════════════";
    }
}
