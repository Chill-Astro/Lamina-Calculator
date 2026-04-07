using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class DiagonalPage : Page
    {
        public DiagonalPage()
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

            InputA.Visibility = InputB.Visibility = InputC.Visibility = Visibility.Collapsed;
            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            switch (shape)
            {
                case "Square":
                    FormulaInfoBar.Message = "d = a√2";
                    SetInputs("Side (a)");
                    break;
                case "Rectangle":
                    FormulaInfoBar.Message = "d = √(L² + W²)";
                    SetInputs("Length (L)", "Width (W)");
                    break;
                case "Cube":
                    FormulaInfoBar.Message = "d = a√3";
                    SetInputs("Side (a)");
                    break;
                case "Cuboid":
                    FormulaInfoBar.Message = "d = √(l² + w² + h²)";
                    SetInputs("Length (l)", "Width (w)", "Height (h)");
                    break;
            }
        }

        private void SetInputs(string a, string b = null, string c = null)
        {
            InputA.Header = a; InputA.Visibility = Visibility.Visible;
            if (b != null) { InputB.Header = b; InputB.Visibility = Visibility.Visible; }
            if (c != null) { InputC.Header = c; InputC.Visibility = Visibility.Visible; }
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;

            // Reset colors to default theme values
            ResultLabel.Foreground = (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];
            ResultValueText.Foreground = (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"];

            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double a = InputA.Value;
            double b = InputB.Value;
            double c = InputC.Value;
            double diagonal = 0;

            // Validation: Show Red Error
            if (double.IsNaN(a) ||
               (InputB.Visibility == Visibility.Visible && double.IsNaN(b)) ||
               (InputC.Visibility == Visibility.Visible && double.IsNaN(c)))
            {
                ShowError("Error:", "Invalid inputs.");
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                switch (shape)
                {
                    case "Square": diagonal = a * Math.Sqrt(2); break;
                    case "Rectangle": diagonal = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)); break;
                    case "Cube": diagonal = a * Math.Sqrt(3); break;
                    case "Cuboid": diagonal = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) + Math.Pow(c, 2)); break;
                }

                ResultLabel.Text = "Diagonal Length =";
                ResultValueText.Text = diagonal.ToString("N2");
            }
            catch
            {
                ShowError("Error:", "Calculation error.");
            }

            await ResultDialog.ShowAsync();
        }

        private void ShowError(string label, string message)
        {
            ResultLabel.Text = label;
            ResultLabel.Foreground = new SolidColorBrush(Colors.Red);
            ResultValueText.Text = message;
            ResultValueText.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ResultValueText.Text)) return;
            var dp = new DataPackage();
            dp.SetText(ResultValueText.Text);
            Clipboard.SetContent(dp);
        }
    }
}