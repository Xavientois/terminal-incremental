using System;

public static class Machine
{
    public enum Type:int
    {
        PC = 0,
        Workstation,
        Server,
        Mainframe,
        Supercomputer,
        DataCentre,
    }

    public static string Name(Type type)
    {
        switch (type)
        {
            case Type.DataCentre:
                return "Data Centre";
            default:
                return type.ToString();
        }
    }

    public static string ShortName(Type type)
    {
        switch (type)
        {
            case Type.DataCentre:
                return "Data Cntr";
            case Type.Supercomputer:
                return "Supercomp";
            case Type.Workstation:
                return "Workstn";
            default:
                return type.ToString();
        }
    }

    public static int TypeCount()
    {
        return Enum.GetNames(typeof(Type)).Length;
    }
}