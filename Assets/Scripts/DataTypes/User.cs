using System.Collections.Generic;
using System;

[Serializable]
public class User
{
    public string username;
    public string password;

    public uint[] machineCounts = new uint[Machine.TypeCount()];
    public float[] machineHackTime = new float[Machine.TypeCount()];

    public HashSet<string> unlockedCommands;
    public HashSet<string> unlockedTransitions;

    public bool[] machineCountVisible = new bool[Machine.TypeCount()];
    private bool machineDisplayVisible = false;

    public User(string usernameIn, string passwordIn, GameConfig config)
    {
        username = usernameIn;
        password = passwordIn;
        unlockedCommands = new HashSet<string>(config.defaultCommands);
        unlockedTransitions = new HashSet<string>(config.defaultTransitions);

        for (int i = 0; i < Machine.TypeCount(); i++)
        {
            machineHackTime[i] = config.defaultHackTime * (float)Math.Pow(config.hackTimeExpBase, i);
        }
    }

    public void AddMachines(Machine.Type type, uint num)
    {
        machineDisplayVisible = true;
        machineCountVisible[(int)type] = true;
        machineCounts[(int)type] += num;
    }

    public bool SpendMachines(Machine.Type type, uint num)
    {
        if (machineCounts[(int)type] >= num)
        {
            machineDisplayVisible = true;
            machineCountVisible[(int)type] = true;
            machineCounts[(int)type]  -= num;

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMachineDisplayVisible()
    {
        return machineDisplayVisible;
    }
}
