using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NCalc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lamina.Views;

public sealed partial class DyNamoPage : Page
{
    private LaminaScript _currentScript;
    private readonly List<NumberBox> _activeInputs = new();

    public DyNamoPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is string path)
        {
            LoadModule(path);
        }
    }

    private void LoadModule(string path)
    {
        try
        {
            _activeInputs.Clear();
            string json = File.ReadAllText(path);
            _currentScript = JsonSerializer.Deserialize<LaminaScript>(json);

            if (_currentScript == null) return;

            // 2. Setup Formula Bar (The fix you requested)
            if (_currentScript.UI != null && !string.IsNullOrEmpty(_currentScript.UI.Formula))
            {
                FormulaBar.Message = _currentScript.UI.Formula;
                FormulaBar.Visibility = Visibility.Visible;
                FormulaBar.IsOpen = true; // CRITICAL: InfoBar won't show without this
            }
            else
            {
                FormulaBar.Visibility = Visibility.Collapsed;
                FormulaBar.IsOpen = false;
            }

            // 3. Load Inputs
            InputList.ItemsSource = _currentScript.UI.Inputs;
        }
        catch (Exception ex)
        {            
            System.Diagnostics.Debug.WriteLine($"DyNamo Load Error: {ex.Message}");
        }
    }

    private void NumberBox_Loaded(object sender, RoutedEventArgs e)
    {
        if (sender is NumberBox nb && nb.Tag != null)
        {
            if (!_activeInputs.Contains(nb))
                _activeInputs.Add(nb);
        }
    }

    private async void Calculate_Click(object sender, RoutedEventArgs e)
    {
        if (_currentScript?.Logic == null) return;

        try
        {
            var expr = new NCalc.Expression(_currentScript.Logic.Output);

            foreach (var nb in _activeInputs)
            {
                // Assign parameters to NCalc from our tracked NumberBoxes
                expr.Parameters[nb.Tag.ToString()] = double.IsNaN(nb.Value) ? 0.0 : nb.Value;
            }

            var result = expr.Evaluate();
            await ShowResult(result.ToString());
        }
        catch
        {
            await ShowResult(_currentScript.Logic.Error ?? "Calculation Error");
        }
    }

    private async Task ShowResult(string value)
    {
        ResultLabel.Text = "Calculated Value";
        ResultValueText.Text = value;

        // This connects the dialog to the window so it can actually pop up
        ResultDialog.XamlRoot = this.Content.XamlRoot;
        await ResultDialog.ShowAsync();
    }
}