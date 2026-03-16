// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExtensibleAPI;


// Main program flow:
// Test Addition operation
Addition addition = new Addition();
addition.Arguments[0] = 5;
addition.Arguments[1] = 10;
Console.WriteLine(addition.PrintResult());




//2. Aquire the calculator operation set


//3. Present the user with a menu of operations and prompt them to
//select one recursively until they select a valid one





//1. Create build-in calculator operations (add, subtract, multiply, divide) 
public class Addition : IOperation {
    public string Name { get; init; } = "Addition";
    private string[] _argumentNames = new string[2];
    private double[] _arguments = new double[2];
    public string[] ArgumentNames {
        get => _argumentNames;
        init {
            if (value.Length != 2) {
                throw new ArgumentException("Addition operation requires exactly 2 arguments.");
            }
            _argumentNames = value;
        }
    }
    public double[] Arguments => _arguments;
    public double Calculate(params List<double> arguments) {
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