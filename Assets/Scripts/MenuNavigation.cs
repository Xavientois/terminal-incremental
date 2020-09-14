using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public Menu currentMenu;

    Dictionary<string, Menu> transitionDictionary = new Dictionary<string, Menu>();
    GameController controller;

    private void Awake()
    {
        controller = GetComponent<GameController>();
    }

    public void UnpackTransitionsInMenu()
    {
        for (int i = 0; i < currentMenu.transitions.Length; i++)
        {
            transitionDictionary.Add(currentMenu.transitions[i].keyString, currentMenu.transitions[i].valueMenu);
            controller.interactionDescriptionsInMenu.Add(currentMenu.transitions[i].transitionDescription);
        }
    }

    public void TransitionToMenu(Menu menu)
    {
        currentMenu = menu;
        controller.DisplayMenuText();
    }

    public void AttemptToTransitionMenu(string transitionKey)
    {
        if (transitionDictionary.ContainsKey(transitionKey)
            && controller.currentUser.unlockedTransitions.Contains(transitionKey))
        {
            TransitionToMenu(transitionDictionary[transitionKey]);
        }
        else
        {
            controller.PrintLn("Unrecognized menu name: " + transitionKey);
            controller.PrintLn("Available menus:");
            foreach (var transition in transitionDictionary)
            {
                if (controller.currentUser.unlockedTransitions.Contains(transition.Key))
                {
                    controller.PrintLn(transition.Key);
                }
            }
        }
    }

    public void ClearTransitions()
    {
        transitionDictionary.Clear();
    }
}
