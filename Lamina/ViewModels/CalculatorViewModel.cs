using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lamina.ViewModels;

public partial class CalculatorViewModel : ObservableRecipient
{
    [ObservableProperty]
    private string _displayText = "0";

    [ObservableProperty]
    private string _operationText = "";

    private double _currentNumber;
    private double _previousNumber;
    private double _secondNumber;
    private string _currentOperator = "";
    private bool _isNewNumberInput = true;
    private bool _divisionByZeroOccurred = false;
    private bool _lastOperationWasEquals = false;

    public ObservableCollection<string> CalculationHistory { get; } = new();

    public CalculatorViewModel()
    {
    }

    [RelayCommand]
    private void InputNumber(string number)
    {
        if (_lastOperationWasEquals)
        {
            ClearAll();
            _lastOperationWasEquals = false;
        }

        if (_isNewNumberInput || DisplayText == "0")
        {
            DisplayText = number;
            _isNewNumberInput = false;
        }
        else if (DisplayText.Length < 16)
        {
            DisplayText += number;
        }

        UpdateCurrentNumber();
    }

    [RelayCommand]
    private void InputDecimal()
    {
        if (_lastOperationWasEquals)
        {
            ClearAll();
            _lastOperationWasEquals = false;
        }

        if (!DisplayText.Contains("."))
        {
            DisplayText += ".";
            _isNewNumberInput = false;
        }
    }

    [RelayCommand]
    private void SetOperator(string op)
    {
        if (_lastOperationWasEquals)
        {
            _previousNumber = _currentNumber;
            _lastOperationWasEquals = false;
        }
        else if (!_isNewNumberInput)
        {
            if (!string.IsNullOrEmpty(_currentOperator))
                CalculateIntermediate();
            else
                _previousNumber = _currentNumber;
        }

        _currentOperator = op;
        OperationText = $"{_previousNumber} {op} ";
        _isNewNumberInput = true;
    }

    [RelayCommand]
    private void Calculate()
    {
        if (string.IsNullOrEmpty(_currentOperator) || _divisionByZeroOccurred) return;

        if (!_lastOperationWasEquals)
            _secondNumber = _currentNumber;
        else
            _previousNumber = _currentNumber;

        double result = PerformMath(_previousNumber, _secondNumber, _currentOperator);

        if (!_divisionByZeroOccurred)
        {
            OperationText = $"{_previousNumber} {_currentOperator} {_secondNumber} =";
            DisplayText = result.ToString("G15");
            CalculationHistory.Add($"{OperationText} {DisplayText}");
            _currentNumber = result;
            _lastOperationWasEquals = true;
            _isNewNumberInput = true;
        }
    }

    [RelayCommand]
    private void ClearAll()
    {
        DisplayText = "0";
        OperationText = "";
        _currentNumber = 0;
        _previousNumber = 0;
        _currentOperator = "";
        _isNewNumberInput = true;
        _divisionByZeroOccurred = false;
        _lastOperationWasEquals = false;
    }

    [RelayCommand]
    private void Backspace()
    {
        if (_isNewNumberInput || DisplayText == "0") return;

        DisplayText = DisplayText.Length > 1 ? DisplayText[..^1] : "0";
        if (DisplayText == "-") DisplayText = "0";

        UpdateCurrentNumber();
    }

    [RelayCommand]
    private void Percent()
    {
        if (!double.TryParse(DisplayText.Replace(",", ""), out double value)) return;
     
        if (!string.IsNullOrEmpty(_currentOperator))
        {
            // For Addition and Subtraction: Calculate percentage relative to the first number
            // Example: 50 - 2% -> becomes 50 - (50 * 0.02) = 50 - 1
            if (_currentOperator == "+" || _currentOperator == "-")
            {
                _currentNumber = _previousNumber * (value / 100.0);
            }
            // For Multiplication and Division: Just treat it as a decimal
            // Example: 50 × 10% -> becomes 50 × 0.1
            else if (_currentOperator == "×" || _currentOperator == "÷")
            {
                _currentNumber = value / 100.0;
            }

            // Update the display and operation string to show the calculated value
            DisplayText = _currentNumber.ToString("G15");
            OperationText = $"{_previousNumber} {_currentOperator} {DisplayText}";
        }
        else
        {
            // If no operator is active, just turn the current number into its decimal form
            // Example: 50% -> 0.5
            _currentNumber = value / 100.0;
            DisplayText = _currentNumber.ToString("G15");
            OperationText = $"{value}%";
        }

        _isNewNumberInput = true; // Ready for the next number or Equals
    }

    [RelayCommand]
    private void ClearEntry() => DisplayText = "0";

    [RelayCommand]
    private void Square()
    {
        if (double.TryParse(DisplayText, out double value))
        {
            double result = value * value;
            OperationText = $"sqr({value})"; 
            DisplayText = result.ToString("G15");
            _currentNumber = result;
            _isNewNumberInput = true;
        }
    }

    [RelayCommand]
    private void SquareRoot()
    {
        if (double.TryParse(DisplayText, out double value))
        {
            if (value < 0)
            {
                DisplayText = "Invalid Input";
                OperationText = $"sqrt({value})";
                return;
            }
            double result = Math.Sqrt(value);
            OperationText = $"sqrt({value})"; 
            DisplayText = result.ToString("G15");
            _currentNumber = result;
            _isNewNumberInput = true;
        }
    }

    [RelayCommand]
    private void CubeRoot()
    {
        if (double.TryParse(DisplayText, out double value))
        {
            double result = Math.Pow(value, 1.0 / 3.0);
            OperationText = $"cbrt({value})"; 
            DisplayText = result.ToString("G15");
            _currentNumber = result;
            _isNewNumberInput = true;
        }
    }

    private void UpdateCurrentNumber()
    {
        if (double.TryParse(DisplayText.Replace(",", ""), out double val))
        {
            _currentNumber = val;
            if (!DisplayText.Contains("."))
                DisplayText = _currentNumber.ToString("N0");
        }
    }

    private void CalculateIntermediate()
    {
        _currentNumber = PerformMath(_previousNumber, _currentNumber, _currentOperator);
        _previousNumber = _currentNumber;
        DisplayText = _currentNumber.ToString("G15");
        _isNewNumberInput = true;
    }

    private double PerformMath(double n1, double n2, string op)
    {
        _divisionByZeroOccurred = false;
        double res = op switch
        {
            "+" => n1 + n2,
            "-" => n1 - n2,
            "×" => n1 * n2,
            "÷" => n2 == 0 ? TriggerDivError() : n1 / n2,
            "^" => Math.Pow(n1, n2),
            _ => n2
        };
        return _divisionByZeroOccurred ? 0 : Math.Round(res, 15);
    }

    private double TriggerDivError()
    {
        _divisionByZeroOccurred = true;
        DisplayText = "Not Defined";
        OperationText = "";
        return 0;
    }
}