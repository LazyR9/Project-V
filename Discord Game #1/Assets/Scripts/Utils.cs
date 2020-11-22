using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static bool isCoroutineExecuting = false;

    public static string GetSecret(string secretName)
    {   
        try
        {
            return System.IO.File.ReadAllText(System.IO.Path.Combine("Secrets", secretName + ".secret"));
        }
        catch
        {
            return null;
        }
    }

    public static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }

    public static IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }
}
