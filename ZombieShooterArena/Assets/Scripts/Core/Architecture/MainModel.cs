using System.Collections.Generic;
using Core.Architecture;


public static class MainModel
{
    public static List<AccessBehaviour> CommonBehaviours = new List<AccessBehaviour>();

    public static T GetAssignedClass<T>() where T : AccessBehaviour
    {
        foreach (object o in CommonBehaviours)
        {
            if (o is T)
            {
                return (T) o;
            }
        }

        return null;
    }
}