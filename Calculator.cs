using System;

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide
}

public class Calculator
{
    private double _num1;
    private double _num2;
    private Operation _operation;

    public Calculator(double num1, double num2, Operation operation)
    {
        _num1 = num1;
        _num2 = num2;
        _operation = operation;
    }

    public double Num1
    {
        get { return _num1; }
        set { _num1 = value; }
    }

    public double Num2
    {
        get { return _num2; }
        set { _num2 = value; }
    }

    public Operation Operation
    {
        get { return _operation; }
        set { _operation = value; }
    }

    public double Calculate()
    {
        switch (_operation)
        {
            case Operation.Add:
                return _num1 + _num2;

            case Operation.Subtract:
                return _num1 - _num2;

            case Operation.Multiply:
                return _num1 * _num2;

            case Operation.Divide:
                if (_num2 != 0)
                    return _num1 / _num2;
                else
                    throw new DivideByZeroException("Cannot divide by zero.");

            default:
                throw new InvalidOperationException("Invalid operation.");
        }
    }
}