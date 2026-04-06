using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class CIPage : Page
    {
        static string temp = "";
        public CIPage() => this.InitializeComponent();

        private async void CalculateCIButton_Click(object sender, RoutedEventArgs e)
        {
            double p = CIPrincipalNumberBox.Value;
            double r = CIRateNumberBox.Value;
            double t = CITimeNumberBox.Value;
            double n = CIFrequencyNumberBox.Value;

            if (double.IsNaN(p) || double.IsNaN(r) || double.IsNaN(t) || double.IsNaN(n))
            {
                temp = "";
                await ShowResultPopup("Input Issue:", "Please fill all fields", false);
                return;
            }

            if (p < 0 || r < 0 || t < 0 || n <= 0)
            {
                temp = "";
                string errorMsg = (n <= 0) ? "f must be > 0" : "Values must be +ve";
                await ShowResultPopup("Invalid Input:", errorMsg, false);
            }
            else
            {
                double rateDec = r / 100.0;
                double amount = p * Math.Pow((1 + (rateDec / n)), (n * t));
                double ci = amount - p;

                temp = ci.ToString("N2");
                await ShowResultPopup("Compound Interest = ", $"{temp} (Total: {amount:N2})", true);
            }
        }

        private async System.Threading.Tasks.Task ShowResultPopup(string context, string result, bool isSuccess)
        {
            ResultLabel.Text = context;
            ResultValueText.Text = result;
            CopyButton.Visibility = isSuccess ? Visibility.Visible : Visibility.Collapsed;

            ResultValueText.Foreground = isSuccess
                ? (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"]
                : (Brush)Application.Current.Resources["SystemFillColorCriticalBrush"];

            ResultDialog.XamlRoot = this.Content.XamlRoot;
            await ResultDialog.ShowAsync();
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(temp)) return;
            var dp = new DataPackage();
            dp.SetText(temp);
            Clipboard.SetContent(dp);
        }
    }
}