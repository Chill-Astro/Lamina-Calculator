using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NCalc;

namespace Lamina.ViewModels;

public partial class AdvancedCalculatorViewModel : ObservableObject
{
    private string _displayText = "";    
    private bool _isInverse;
    private string _angleModeText = "DEG";

    public AdvancedCalculatorViewModel()
    {
        InputNumberCommand = new RelayCommand<string>(InputNumber);
        SetOperatorCommand = new RelayCommand<string>(SetOperator);
        CalculateCommand = new RelayCommand(Calculate);
        ClearAllCommand = new RelayCommand(ClearAll);
        BackspaceCommand = new RelayCommand(Backspace);
        InputDecimalCommand = new RelayCommand(InputDecimal);
        InputCommaCommand = new RelayCommand(InputComma);
        CycleAngleModeCommand = new RelayCommand(CycleAngleMode);
        NegateCommand = new RelayCommand(Negate);
        ReciprocalCommand = new RelayCommand(Reciprocal);
        PasteCommand = new RelayCommand<string>(Paste);
    }

    #region Properties

    public string DisplayText
    {
        get => _displayText;
        set => SetProperty(ref _displayText, value);
    }

    public string AngleModeText
    {
        get => _angleModeText;
        set => SetProperty(ref _angleModeText, value);
    }

    public bool IsInverse
    {
        get => _isInverse;
        set
        {
            if (SetProperty(ref _isInverse, value))
            {
                OnPropertyChanged(nameof(SinLabel));
                OnPropertyChanged(nameof(CosLabel));
                OnPropertyChanged(nameof(TanLabel));
                OnPropertyChanged(nameof(SinParameter));
                OnPropertyChanged(nameof(CosParameter));
                OnPropertyChanged(nameof(TanParameter));
            }
        }
    }

    public string SinLabel => IsInverse ? "sin⁻¹" : "sin";
    public string CosLabel => IsInverse ? "cos⁻¹" : "cos";
    public string TanLabel => IsInverse ? "tan⁻¹" : "tan";

    public string SinParameter => IsInverse ? "Asin(" : "Sin(";
    public string CosParameter => IsInverse ? "Acos(" : "Cos(";
    public string TanParameter => IsInverse ? "Atan(" : "Tan(";

    #endregion

    #region Commands

    public ICommand InputNumberCommand { get; }
    public ICommand SetOperatorCommand { get; }
    public ICommand CalculateCommand { get; }
    public ICommand ClearAllCommand { get; }
    public ICommand BackspaceCommand { get; }
    public ICommand InputDecimalCommand { get; }
    public ICommand InputCommaCommand { get; }
    public ICommand CycleAngleModeCommand { get; }
    public ICommand NegateCommand { get; }
    public ICommand ReciprocalCommand { get; }
    public ICommand PasteCommand { get; }

    #endregion

    #region Logic

    private void InputNumber(string input)
    {
        if (DisplayText == "" || DisplayText == "Error")
            DisplayText = input;
        else
            DisplayText += input;
    }

    private void SetOperator(string op)
    {
        string nCalcOp = op switch
        {
            "×" => "*",
            "÷" => "/",
            "^" => "Pow(",
            _ => op
        };

        if ((DisplayText == "" || DisplayText == "Error") && nCalcOp == "-")
            DisplayText = "-";
        else if (DisplayText == "" && nCalcOp == "Pow(")
            DisplayText = "Pow(";
        else
            DisplayText += nCalcOp;
    }

    private void Negate()
    {
        if (DisplayText == "" || DisplayText == "Error") return;

        // Wrap the existing expression in a negative bracket
        if (DisplayText.StartsWith("-(") && DisplayText.EndsWith(")"))
            DisplayText = DisplayText.Substring(2, DisplayText.Length - 3);
        else
            DisplayText = $"-({DisplayText})";
    }

    private void Reciprocal()
    {
        if (DisplayText == "" || DisplayText == "Error") return;
        DisplayText = $"1/({DisplayText})";
    }

    private void InputDecimal()
    {
        var parts = DisplayText.Split(new[] { '+', '-', '*', '/', '(', ')', ',' });
        var lastPart = parts.LastOrDefault();
        if (lastPart != null && !lastPart.Contains("."))
            DisplayText += ".";
    }

    private void InputComma()
    {
        if (DisplayText != "" && !DisplayText.EndsWith(","))
            DisplayText += ",";
    }

    private void Backspace()
    {
        if (DisplayText.Length > 0 && DisplayText != "Error")
        {
            DisplayText = DisplayText.Substring(0, DisplayText.Length - 1);
            if (string.IsNullOrEmpty(DisplayText)) DisplayText = "";
        }
        else
            DisplayText = "";
    }

    private void ClearAll()
    {
        DisplayText = "";        
    }

    private void CycleAngleMode()
    {
        AngleModeText = AngleModeText == "DEG" ? "RAD" : "DEG";
    }

    private void Paste(string text)
    {
        if (double.TryParse(text, out _))
            DisplayText = text;
    }

    private void Calculate()
    {
        try
        {
            string expressionToEvaluate = DisplayText;

            // 1. Handle Cube Root symbol replacement before NCalc sees it
            // We turn ³√(x) into Cbrt(x) which we will define in EvaluateFunction
            if (expressionToEvaluate.Contains("³√("))
            {
                expressionToEvaluate = expressionToEvaluate.Replace("³√(", "Cbrt(");
            }

            // Auto-close parentheses for NCalc safety
            int openBrackets = expressionToEvaluate.Count(f => f == '(');
            int closeBrackets = expressionToEvaluate.Count(f => f == ')');
            for (int i = 0; i < (openBrackets - closeBrackets); i++)
                expressionToEvaluate += ")";

            // Handle Factorial replacement
            if (expressionToEvaluate.Contains("!"))
            {
                expressionToEvaluate = expressionToEvaluate.Replace("!", "");
                expressionToEvaluate = $"Fact({expressionToEvaluate})";
            }

            var expression = new Expression(expressionToEvaluate);

            expression.Parameters["Pi"] = Math.PI;
            expression.Parameters["E"] = Math.E;

            bool isDeg = AngleModeText == "DEG";
            expression.EvaluateFunction += (name, args) =>
            {
                var parameters = args.EvaluateParameters();
                double val = Convert.ToDouble(parameters[0]);

                switch (name)
                {
                    // Existing cases...
                    case "Sin":
                        args.Result = Math.Sin(isDeg ? val * Math.PI / 180.0 : val);
                        break;
                    case "Cos":
                        args.Result = Math.Cos(isDeg ? val * Math.PI / 180.0 : val);
                        break;
                    case "Tan":
                        args.Result = Math.Tan(isDeg ? val * Math.PI / 180.0 : val);
                        break;
                    case "Asin":
                        double asin = Math.Asin(val);
                        args.Result = isDeg ? asin * 180.0 / Math.PI : asin;
                        break;
                    case "Acos":
                        double acos = Math.Acos(val);
                        args.Result = isDeg ? acos * 180.0 / Math.PI : acos;
                        break;
                    case "Atan":
                        double atan = Math.Atan(val);
                        args.Result = isDeg ? atan * 180.0 / Math.PI : atan;
                        break;
                    case "Fact":
                        args.Result = GetFactorial(val);
                        break;

                    // 2. Add the Cube Root Logic here
                    case "Cbrt":
                        // Using Pow(x, 1/3) or Math.Cbrt in newer .NET versions
                        args.Result = Math.Pow(val, 1.0 / 3.0);
                        break;
                }
            };

            var result = expression.Evaluate();
            DisplayText = result.ToString();
        }
        catch
        {
            DisplayText = "Error";
        }
    }

    private double GetFactorial(double n)
    {
        if (n < 0) return double.NaN;
        return n <= 1 ? 1 : n * GetFactorial(n - 1);
    }

    #endregion
}