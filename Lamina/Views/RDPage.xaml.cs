using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class RDPage : Page
    {
        public RDPage()
        {
            this.InitializeComponent();
        }

        private async void CalculateRDButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure XamlRoot is set early to prevent crashes
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;

            double p = MonthlyDepositNumberBox.Value;
            double r = RateNumberBox.Value;
            double n = MonthsNumberBox.Value;

            // --- ERROR HANDLING ---
            if (double.IsNaN(p) || double.IsNaN(r) || double.IsNaN(n) || n <= 0 || p <= 0)
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Please enter valid positive numbers for all fields.";
                CopyButton.Visibility = Visibility.Collapsed; // Hide copy button on error
                await ResultDialog.ShowAsync();
                return;
            }

            // --- CALCULATION ---
            // RD Interest Formula: P * [n(n+1)/2] * (r/12) * (1/100)
            double totalInterest = p * (n * (n + 1) / 2.0) * (r / 12.0) * (1.0 / 100.0);
            double totalPrincipal = p * n;
            double maturityAmount = totalPrincipal + totalInterest;

            // --- SUCCESS VIEW ---
            ResultLabel.Text = "Total Maturity Amount:";
            ResultValueText.Text = maturityAmount.ToString("N2");
            CopyButton.Visibility = Visibility.Visible; // Show copy button for valid result

            await ResultDialog.ShowAsync();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ResultValueText.Text) || ResultLabel.Text == "Error:") return;

            var dataPackage = new DataPackage();
            dataPackage.SetText(ResultValueText.Text);
            Clipboard.SetContent(dataPackage);
        }
    }
}