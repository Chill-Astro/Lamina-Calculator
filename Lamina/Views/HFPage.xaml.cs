using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class HFPage : Page
    {
        string resultTemp = "";

        public HFPage()
        {
            this.InitializeComponent();
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;

            // Reset colors to default
            ResultLabel.Foreground = (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];
            ResultValueText.Foreground = (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            CopyButton.Visibility = Visibility.Visible;

            double a = SideANumberBox.Value;
            double b = SideBNumberBox.Value;
            double c = SideCNumberBox.Value;

            // 1. Validation for Missing/Zero/Negative
            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c) || a <= 0 || b <= 0 || c <= 0)
            {
                ShowError("Error:", "Invalid side lengths.");
                await ResultDialog.ShowAsync();
                return;
            }

            // 2. Triangle Inequality Check
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                ShowError("Invalid Geometry:", "Not a valid triangle.");
                await ResultDialog.ShowAsync();
                return;
            }

            // 3. Calculation
            try
            {
                double s = (a + b + c) / 2.0;
                double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

                if (double.IsNaN(area) || double.IsInfinity(area)) throw new Exception();

                resultTemp = area.ToString("N2");
                ResultLabel.Text = "Area =";
                ResultValueText.Text = resultTemp;
            }
            catch
            {
                ShowError("Error:", "Calculation failed.");
            }

            await ResultDialog.ShowAsync();
        }

        private void ShowError(string label, string message)
        {
            ResultLabel.Text = label;
            ResultLabel.Foreground = new SolidColorBrush(Colors.Red);
            ResultValueText.Text = message;
            ResultValueText.Foreground = new SolidColorBrush(Colors.Red);
            CopyButton.Visibility = Visibility.Collapsed; // Hide copy on error
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