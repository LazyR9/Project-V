using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class File : MonoBehaviour
{
    public string fileName;
    public byte[] data;
    public Directory directory;

    public string GetPath()
    {
        return directory.GetPath() + "\\" + name;
    }
}
