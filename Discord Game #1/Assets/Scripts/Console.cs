using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public string cd = "$";

    string consoleHistory = "";
    InputField inputField;

    ProjectV controls;
    public GameManager gameManager;

    void Awake()
    {
        controls = gameManager.controls;

        controls.UI.Submit.performed += ctx =>
        {
            if (inputField.isFocused)
            {
                Debug.Log("command sent");
                string command = inputField.text.Replace(consoleHistory, "");
                ProcessCommand(command);
            }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponentInChildren<InputField>();
        AddToConsole(string.Format("(user)@(pc):{0} ", cd));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessCommand(string input)
    {
        string command = input.Split(' ')[0];
        List<string> args = new List<string>(input.Split(' '));
        args.RemoveAt(0);

        Commands commands = new Commands();
        //string output = (string)
        commands.GetType().GetMethod(command).Invoke(command, args.ToArray());

        //AddToConsole(output);
        AddToConsole(string.Format("(user)@(pc):{0} ", cd));
    }

    public void AddToConsole(string message)
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
