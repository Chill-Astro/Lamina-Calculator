using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class SIPage : Page
    {
        static string temp = "";
        public SIPage() => this.InitializeComponent();

        private async void CalculateSIButton_Click(object sender, RoutedEventArgs e)
        {
            double p = PrincipalNumberBox.Value;
            double r = RateNumberBox.Value;
            double t = TimeNumberBox.Value;

            if (double.IsNaN(p) || double.IsNaN(r) || double.IsNaN(t))
            {
                temp = "";
                await ShowResultPopup("Input Issue:", "Please fill all fields", false);
                return;
            }

            if (p < 0 || r < 0 || t < 0)
            {
                temp = "";
                await ShowResultPopup("Invalid Input:", "Values must be +ve", false);
            }
            else
            {
                double si = (p * r * t) / 100.0;
                double total = p + si;

                // Using N2 for clean number formatting (1,234.56)
                temp = si.ToString("N2");
                await ShowResultPopup("Interest Accrued:", $"{temp} (Total: {total:N2})", true);
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