using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameConfig config;

    public InputField inputField;
    public string currentUsername;
    public User currentUser;
    public GameObject machineCountPanel;

    [HideInInspector] public MenuNavigation menuNavigation;
    [HideInInspector] public PrintHandler printHandler;
    [HideInInspector] public LoadHandler loadHandler;
    [HideInInspector] public UnlockHandler unlockHandler;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public List<string> interactionDescriptionsInMenu = new List<string>();
    [HideInInspector] public Dictionary<string, User> users = new Dictionary<string, User>();

    void Awake()
    {
        menuNavigation = GetComponent<MenuNavigation>();
        printHandler = GetComponent<PrintHandler>();
        loadHandler = GetComponent<LoadHandler>();
        unlockHandler = GetComponent<UnlockHandler>();
        audioSource = GetComponent <AudioSource>();
    }

    void Start()
    {
        DisplayMenuText();
    }

    public void AddMachines(Machine.Type type, uint num)
    {
        currentUser.AddMachines(type, num);

        UpdateMachineCountDisplay(type);
    }

    public bool SpendMachines(Machine.Type type, uint num)
    {
        bool success = currentUser.SpendMachines(type, num);

        if (success)
        {
            UpdateMachineCountDisplay(type);
        }

        return success;
    }

    private void UpdateMachineCountDisplay(Machine.Type type)
    {
        machineCountPanel.SetActive(currentUser.IsMachineDisplayVisible());

        if (currentUser.machineCountVisible[(int)type])
        {
            // Activate display if we have started to get this machine type
            MachineCountDisplay counterDisplay = Array.Find(machineCountPanel.GetComponentsInChildren<MachineCountDisplay>(true), counter => counter.machineType == type);

            if (counterDisplay)
            {
                counterDisplay.gameObject.SetActive(true);
                counterDisplay.user = currentUser;
                counterDisplay.UpdateText();
            }
            else
            {
                Debug.LogError("Missing counter for machine type " + Machine.Name(type));
            }
        }        
    }

    private void UpdateAllMachineCountDisplays()
    {
        foreach (Machine.Type machineType in Enum.GetValues(typeof(Machine.Type)))
        {
            UpdateMachineCountDisplay(machineType);
        }
    }

    public void Login(string username)
    {
        currentUser = users[username];
        UpdateAllMachineCountDisplays();
    }

    public void Logout()
    {
        machineCountPanel.SetActive(false);

        users[currentUsername] = currentUser;
        currentUser = null;
        currentUsername = null;
    }

    public void DisplayMenuText()
    {
        ClearCollectionsForNewMenu();

        UnpackMenu();

        string joinedInteractionsDescriptions = string.Join("\n", interactionDescriptionsInMenu.ToArray());

        string combinedText = menuNavigation.currentMenu.description + "\n" + joinedInteractionsDescriptions;

        PrintLn(combinedText);
    }

    private void UnpackMenu()
    {
        menuNavigation.UnpackTransitionsInMenu();
    }

    void ClearCollectionsForNewMenu()
    {
        interactionDescriptionsInMenu.Clear();
        menuNavigation.ClearTransitions();
    }

    public void DelayedPrint(float delaySeconds, string commandToAdd)
    {
        printHandler.DelayedPrint(delaySeconds, commandToAdd);
    }

    public void DelayedPrintLn(float delaySeconds, string commandToAdd)
    {
        DelayedPrint(delaySeconds, commandToAdd + "\n");
    }

    public void PrintLn(string commandToAdd)
    {
        DelayedPrintLn(0, commandToAdd);
    }

    public void PrintLn()
    {
        DelayedPrintLn(0, "");
    }

    public void Print(string commandToAdd)
    {
        DelayedPrint(0, commandToAdd);
    }

    // Perform an action in a certain number of seconds
    public void LoadAction(float seconds, Action action)
    {
        loadHandler.BlockLoadAction(seconds, inputField.characterLimit, action);
    }

    public Command[] GetAvailableCommands()
    {
        return menuNavigation.currentMenu.availableCommands;
    }

    public bool HandleCommand(string[] arguments)
    {
        if (arguments.Length == 0)
        {
            return true;
        }

        Command[] availableCommands = menuNavigation.currentMenu.availableCommands;

        for (int i = 0; i < availableCommands.Length; i++)
        {
            Command command = availableCommands[i];

            if ((command.keyword == arguments[0] && currentUser.unlockedCommands.Contains(command.keyword))
                || command.keyword == "")
            {
                command.RespondToInput(this, arguments);
                return true;
            }
        }

        return false;
    }

    public bool IsHistoryEnabled()
    {
        return menuNavigation.currentMenu.historyEnabled;
    }

    public bool ShouldEchoInput()
    {
        return menuNavigation.currentMenu.echoInput;
    }

    public bool ShouldSplitInput()
    {
        return menuNavigation.currentMenu.shouldSplitInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
