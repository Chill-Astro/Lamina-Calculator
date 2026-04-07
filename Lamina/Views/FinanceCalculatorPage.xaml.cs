using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class FinanceCalculatorPage : Page
    {
        public FinanceCalculatorPage()
        {
            this.InitializeComponent();
            this.Loaded += Page_Loaded;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Ensure the first item is selected and UI is synced on load
            ComboBox.SelectedIndex = 0;
            UpdateUI();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (ComboBox == null || InputA == null || FormulaInfoBar == null) return;

            // Reset all inputs to collapsed
            InputA.Visibility = InputB.Visibility = InputC.Visibility = Visibility.Collapsed;

            string type = (ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (string.IsNullOrEmpty(type)) return;

            switch (type)
            {
                case "Simple Interest":
                    FormulaInfoBar.Message = "I = (P × R × T) / 100";
                    SetInputs("Principal (P)", "Rate % (R)", "Time in Years (T)");
                    break;
                case "Compound Interest":
                    FormulaInfoBar.Message = "A = P(1 + r/100)^t";
                    SetInputs("Principal (P)", "Rate % (r)", "Time in Years (t)");
                    break;
                case "Recurring Deposit":
                    FormulaInfoBar.Message = "I = [P × n(n+1) / 24] × r/100";
                    SetInputs("Monthly Deposit (P)", "Rate % (r)", "Total Months (n)");
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

            string type = (ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double p = InputA.Value;
            double r = InputB.Value;
            double t = InputC.Value;

            double interest = 0;
            double totalAmount = 0;

            // Basic validation
            if (double.IsNaN(p) || double.IsNaN(r) || double.IsNaN(t))
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Please fill all fields.";
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                switch (type)
                {
                    case "Simple Interest":
                        interest = (p * r * t) / 100.0;
                        totalAmount = p + interest;
                        ResultLabel.Text = $"Total Maturity: {totalAmount:N2}";
                        ResultValueText.Text = interest.ToString("N2");
                        break;

                    case "Compound Interest":
                        // Annual compounding logic
                        totalAmount = p * Math.Pow((1 + (r / 100.0)), t);
                        interest = totalAmount - p;
                        ResultLabel.Text = $"Interest Earned: {interest:N2}";
                        ResultValueText.Text = totalAmount.ToString("N2");
                        break;

                    case "Recurring Deposit":
                        // p = monthly deposit, t = number of months
                        interest = (p * t * (t + 1) / 24.0) * (r / 100.0);
                        totalAmount = (p * t) + interest;
                        ResultLabel.Text = $"Total Maturity: {totalAmount:N2}";
                        ResultValueText.Text = interest.ToString("N2");
                        break;
                }
            }
            catch
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Calculation failed.";
            }

            await ResultDialog.ShowAsync();
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