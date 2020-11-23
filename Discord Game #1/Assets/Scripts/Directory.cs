using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directory : MonoBehaviour
{
    public string directoryName;
    public Directory parentDirectory;

    public string GetPath()
    {
        if (parentDirectory == null)
        {
            return null;
        }
        return parentDirectory.GetPath() + "\\" + name;
    }
}
