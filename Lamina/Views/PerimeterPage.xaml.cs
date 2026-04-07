using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class PerimeterPage : Page
    {
        public PerimeterPage()
        {
            this.InitializeComponent();
            this.Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ShapeComboBox.SelectedIndex = 0;
            UpdateUI();
        }

        private void ShapeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (ShapeComboBox == null || InputA == null || FormulaInfoBar == null) return;

            InputA.Visibility = InputB.Visibility = Visibility.Collapsed;
            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            switch (shape)
            {
                case "Equilateral Triangle":
                    FormulaInfoBar.Message = "Perimeter = 3 × s";
                    SetInputs("Side (s)");
                    break;
                case "Isosceles Triangle":
                    FormulaInfoBar.Message = "Perimeter = 2a + b";
                    SetInputs("Equal Side (a)", "Base (b)");
                    break;
                case "Square / Rhombus":
                    FormulaInfoBar.Message = "Perimeter = 4 × s";
                    SetInputs("Side (s)");
                    break;
                case "Rectangle / Parallelogram":
                    FormulaInfoBar.Message = "Perimeter = 2 × (L + W)";
                    SetInputs("Length (L)", "Width (W)");
                    break;
                case "Circle":
                    FormulaInfoBar.Message = "Circumference = 2πr";
                    SetInputs("Radius (r)");
                    break;
                case "Semi-circle":
                    FormulaInfoBar.Message = "Perimeter = πr + 2r";
                    SetInputs("Radius (r)");
                    break;
            }
        }

        private void SetInputs(string a, string b = null)
        {
            InputA.Header = a; InputA.Visibility = Visibility.Visible;
            if (b != null) { InputB.Header = b; InputB.Visibility = Visibility.Visible; }
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;

            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double a = InputA.Value;
            double b = InputB.Value;
            double perimeter = 0;

            if (double.IsNaN(a) || (InputB.Visibility == Visibility.Visible && double.IsNaN(b)))
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Please enter valid numbers.";
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                switch (shape)
                {
                    case "Equilateral Triangle": perimeter = 3 * a; break;
                    case "Isosceles Triangle": perimeter = (2 * a) + b; break;
                    case "Square / Rhombus": perimeter = 4 * a; break;
                    case "Rectangle / Parallelogram": perimeter = 2 * (a + b); break;
                    case "Circle": perimeter = 2 * Math.PI * a; break;
                    case "Semi-circle": perimeter = (Math.PI * a) + (2 * a); break;
                }

                ResultLabel.Text = $"Perimeter =";
                ResultValueText.Text = perimeter.ToString("N2");
            }
            catch
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Invalid dimensions.";
            }

            await ResultDialog.ShowAsync();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var dp = new DataPackage();
            dp.SetText(ResultValueText.Text);
            Clipboard.SetContent(dp);
        }
    }
}