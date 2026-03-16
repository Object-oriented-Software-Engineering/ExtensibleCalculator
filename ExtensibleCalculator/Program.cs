// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtensibleAPI;

internal class Program
{
    public static void Main(string[] args)
    {
        // Test Addition operation
        Addition addition = new Addition();
        addition.Arguments[0] = 5;
        addition.Arguments[1] = 10;
        Console.WriteLine(addition.PrintResult());
        while (true) {
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) {
                break;
            }
            // 2. Aquire the calculator operation set
            List<IOperation> operations = GetAvailableOperations();
            int operationindex = 0;
            foreach (IOperation operation in operations) {
                Console.WriteLine($"{operationindex}. {operation.Name}");
                operationindex++;
            }

            Console.Write("Select the operation by entering the corresponding number (or press ESC to exit):");
            string selected = Console.ReadLine();
            int selectedIndex;
            if (Int32.TryParse(selected, out selectedIndex)) {
                // Request arguments for the selected operation
                IOperation selectedOperation = operations[selectedIndex];
                int i = 0;
                foreach (string argument in selectedOperation.ArgumentNames) {
                    Console.Write($"Give {argument}:");
                    string argumentString = Console.ReadLine();
                    double argumentValue;
                    while (!Double.TryParse(argumentString, out argumentValue)) {
                        Console.Write($"Invalid input. Please enter a double number for {argument}:");
                        argumentString = Console.ReadLine();
                    }

                    selectedOperation.Arguments[i] = argumentValue;
                    i++;
                }

                Console.WriteLine(selectedOperation.PrintResult());
            }

        }

    }

    private static List<IOperation> GetAvailableOperations()
    {
        List<IOperation> operations = new List<IOperation>
        {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division()
        };

        // Check the Extensions Project folder for additional operations
        string baseDir = AppContext.BaseDirectory;
        string dataDir = Path.Combine(baseDir,"Extensions");
        if (Directory.Exists(dataDir)) {
            string[] dllFiles = Directory.GetFiles(dataDir, "Extension*.dll");
            foreach (string dllFile in dllFiles) {
                try {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types) {
                        if (typeof(IOperation).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract) {
                            IOperation operationInstance = (IOperation)Activator.CreateInstance(type);
                            operations.Add(operationInstance);
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine($"Failed to load operations from {dllFile}: {ex.Message}");
                }
            }
        }


        return operations;
    }
}

// 1. Create build-in calculator operations (add, subtract, multiply, divide)
public class Addition : IOperation
{
    public string Name { get; init; } = "Addition";

    private string[] _argumentNames = new string[2];
    private readonly double[] _arguments = new double[2];

    public string[] ArgumentNames
    {
        get => _argumentNames;
        init
        {
            if (value.Length != 2)
            {
                throw new ArgumentException("Addition operation requires exactly 2 arguments.");
            }

            _argumentNames = value;
        }
    }

    public double[] Arguments => _arguments;

    public double Calculate(params List<double> arguments)
    {
        return arguments[0] + arguments[1];
    }

    public string PrintResult() {
        return $"{_arguments[0]} + {_arguments[1]} = {Calculate(_arguments[0], _arguments[1])}";
    }

    public Addition() {
        ArgumentNames = new string[] { "Term1", "Term2" };
    }
}

public class Subtraction : IOperation {
    public string Name { get; init; } = "Subtraction";
    private string[] _argumentNames = new string[2];
    private double[] _arguments = new double[2];
    public string[] ArgumentNames {
        get => _argumentNames;
        init {
            if (value.Length != 2) {
                throw new ArgumentException("Subtraction operation requires exactly 2 arguments.");
            }
            _argumentNames = value;
        }
    }
    public double[] Arguments => _arguments;
    public double Calculate(params List<double> arguments) {
        return arguments[0] - arguments[1];
    }
    public string PrintResult() {
        return $"{_arguments[0]} - {_arguments[1]} = {Calculate(_arguments[0], _arguments[1])}";
    }
    public Subtraction() {
        ArgumentNames = new string[] { "Minuend", "Subtrahend" };
    }
}

public class Multiplication : IOperation {
    public string Name { get; init; } = "Multiplication";
    private string[] _argumentNames = new string[2];
    private double[] _arguments = new double[2];
    public string[] ArgumentNames {
        get => _argumentNames;
        init {
            if (value.Length != 2) {
                throw new ArgumentException("Multiplication operation requires exactly 2 arguments.");
            }
            _argumentNames = value;
        }
    }
    public double[] Arguments => _arguments;
    public double Calculate(params List<double> arguments) {
        return arguments[0] * arguments[1];
    }
    public string PrintResult() {
        return $"{_arguments[0]} * {_arguments[1]} = {Calculate(_arguments[0], _arguments[1])}";
    }
    public Multiplication() {
        ArgumentNames = new string[] { "Factor1", "Factor2" };
    }
}

public class Division : IOperation {
    public string Name { get; init; } = "Division";
    private string[] _argumentNames = new string[2];
    private double[] _arguments = new double[2];
    public string[] ArgumentNames {
        get => _argumentNames;
        init {
            if (value.Length != 2) {
                throw new ArgumentException("Division operation requires exactly 2 arguments.");
            }
            _argumentNames = value;
        }
    }
    public double[] Arguments => _arguments;
    public double Calculate(params List<double> arguments) {
        if (arguments[1] == 0) {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        return arguments[0] / arguments[1];
    }
    public string PrintResult() {
        return $"{_arguments[0]} / {_arguments[1]} = {Calculate(_arguments[0], _arguments[1])}";
    }
    public Division() {
        ArgumentNames = new string[] { "Dividend", "Divisor" };
    }
}