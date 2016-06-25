using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;
public class My
{

    public static void Log(object message)
    {
        Debug.Log(message);
    }

    public static void Log(object message, Object context)
    {
        Debug.Log(message, context);
    }

    public static void LogError(object message)
    {
        Debug.LogError(message);
    }

    public static void LogError(object message, Object context)
    {
        Debug.LogError(message, context);
    }

    public static void assert(bool test, object message)
    {
        if (test) return;
        Debug.Assert(test,message);
    }
}
