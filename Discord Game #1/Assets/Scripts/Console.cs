using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using TMPro;

public class Console : MonoBehaviour
{
    public string cd = "$";

    string consoleHistory = "";
    TMP_InputField inputField;

    ProjectV controls;
    public GameManager gameManager;

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

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
        AddToConsole(string.Format("(user)@(pc):{0} ", cd));
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
        } catch (Exception e)
        {
            output = "An error occurred: " + e.Message;
            Debug.LogError(e);
        }

        AddToConsole(output);
        AddToConsole(string.Format("\n(user)@(pc):{0} ", cd));
    }

    private void AddToConsole(string message)
    {
        inputField.text += message;
        consoleHistory += message;
    }

    public void OnEdit(string str)
    {
        if(!str.Contains(consoleHistory))
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
        controls.UI.Enable();
        controls.Player.Disable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
        controls.Player.Enable();
    }
}
