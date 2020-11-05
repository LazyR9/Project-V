using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commands
{
    // All functions MUST return a string for an output to the console
    
    public string Echo(string message)
    {
        return message;
    }

    public string Cd(string directory)
    {
        GameObject.Find("Console").GetComponent<Console>().cd = directory;
        return "";
    }

}
