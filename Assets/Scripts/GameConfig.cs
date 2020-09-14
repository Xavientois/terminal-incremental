using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HackerGame/Config")]
public class GameConfig : ScriptableObject
{
    [Header("User")]
    public string[] defaultCommands;
    public string[] defaultTransitions;

    [Header("Hacking")]
    public float defaultHackTime = 10;
    public float hackTimeExpBase = 10; // The base of the exponential increase in hacking time

    [Header("Overclocking")]
    public float overclockingMultiplier = 3;
    public float overclockingDecrease = 0.05f;
}
