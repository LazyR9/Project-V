using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDesk.Options;

public class Commands
{
    // Set up a reference to the console
    public Console Console;
    public Commands(Console console)
    {
        Console = console;
    }

    // All functions MUST return a string for an output to the console.
    // They can also use Console.WriteLine() to write to console without returning.
    
    public string Echo(string[] args)
    {
        List<string> input;

        bool showHelp = false;

        var options = new OptionSet()
        {
            {
                "h|help",
                "Displays the help page",
                value => {}
            }
        };

        try
        {
            input = options.Parse(args);
        }
        catch (OptionException e)
        {
            Debug.LogError(e.ToString());

            Console.WriteLine("An error has occurred:");
            Console.WriteLine(e.Message);
            Console.WriteLine("Use \"-h\" or \"--help\" for help");
            return "";
        }
        showHelp = input.Count == 0;
        if (showHelp)
        {
            StringWriter stringWriter = new StringWriter();
            options.WriteOptionDescriptions(stringWriter);

            Console.WriteLine("A basic command to test my custom command system.");
            Console.WriteLine("Usage:");

            return stringWriter.ToString();
        }

        return string.Join(" ", input);
    }

    public string Cd(string directory)
    {
        Console.cd = directory;
        return "";
    }
}
