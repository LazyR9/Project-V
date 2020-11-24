using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    LevelManager levelManager;
    public Directory cd;

    string consoleHistory = "";
    TMP_InputField inputField;

    ProjectV controls;
    public GameManager gameManager;

    WindowBase windowBase;

    void Awake()
    {
        controls = gameManager.controls;

        controls.UI.Submit.performed += ctx =>
        {
            if (inputField.isFocused)
            {
                string command = inputField.text.Replace(consoleHistory, "");
                ProcessCommand(command);
            }
        };
    }

    public void ProcessCommand(string input)
    {
        string output;

        string command = input.Split(' ')[0];
        List<string> args = new List<string>(input.Split(' '));
        args.RemoveAt(0);

        Commands commands = new Commands(this);
        string commandName = command.First().ToString().ToUpper() + command.Substring(1);
        MethodInfo commandMethod = commands.GetType().GetMethod(commandName);
        consoleHistory += input;
        AddToConsole("\n");
        
        try
        {
            output = (string)commandMethod.Invoke(commands, new object[1]{args.ToArray()});
        }
        catch
        {
            output = "Unknow command: " + commandName.ToLower();
            output += "\nPlease type \"help\" for a list of commands.";
        }

        AddToConsole(output);
        AddToConsole(string.Format("\n{0}@({1}: {2} ", levelManager.user, levelManager.pcName, cd));
    }

    private void AddToConsole(string message)
    {
        inputField.caretPosition = inputField.text.Length;

        inputField.text += message;
        consoleHistory += message;
    }

    public void OnEdit(string str)
    {
        if(!str.StartsWith(consoleHistory))
        {
            inputField.text = consoleHistory;
            inputField.caretPosition++;
        }
    }

    public void WriteLine(object line)
    {
        AddToConsole(line.ToString() + "\n");
    }

    public void OnButtonClick()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    void OnEnable()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        cd = levelManager.defaultDirectory;

        inputField = GetComponentInChildren<TMP_InputField>();
        windowBase = GetComponent<WindowBase>();

        transform.localPosition = Vector3.zero;
        Vector2 newSize = new Vector2(windowBase.maxWidth - 100, windowBase.maxHeight - 100);
        GetComponent<RectTransform>().sizeDelta = newSize;

        inputField.text = "";
        AddToConsole(string.Format("\n{0}@{1}: {2} ", levelManager.user, levelManager.pcName, cd.GetPath()));

        controls.UI.Enable();
        controls.Player.Disable();
    }

    void OnDisable()
    {
        consoleHistory = "";

        controls.UI.Disable();
        controls.Player.Enable();
    }
}
