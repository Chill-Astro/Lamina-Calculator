using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class HFPage : Page
    {
        static string temp = "";
        public HFPage()
        {
            this.InitializeComponent();
        }
        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            double a = SideANumberBox.Value;
            double b = SideBNumberBox.Value;
            double c = SideCNumberBox.Value;

            // 1. Check for Missing Input
            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
            {
                temp = string.Empty;
                await ShowResultPopup("Error", "Sides can't be 0", false);
                return;
            }

            // 2. Validate Positive Numbers
            if (a <= 0 || b <= 0 || c <= 0)
            {
                temp = string.Empty;
                await ShowResultPopup("Error", "Sides must be +ve", false);
            }
            // 3. Triangle Inequality Check
            else if (a + b <= c || a + c <= b || b + c <= a)
            {
                temp = string.Empty;
                await ShowResultPopup("Invalid Geometry", "These are not Sides of a Triangle", false);
            }
            else
            {
                // 4. Success Case
                double s = (a + b + c) / 2.0;
                double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));

                temp = area.ToString("F2");
                await ShowResultPopup("Calculation Result", $"{temp} sq. units", true);
            }
        }        
        private async System.Threading.Tasks.Task ShowResultPopup(string title, string message, bool isSuccess)
        {
            ResultDialog.Title = title;
            ResultValueText.Text = message;

            // Hide or show the Copy button and label based on success
            ResultLabel.Visibility = isSuccess ? Visibility.Visible : Visibility.Collapsed;
            CopyButton.Visibility = isSuccess ? Visibility.Visible : Visibility.Collapsed;

            // Set text color: Normal for success, Red for error
            ResultValueText.Foreground = isSuccess
                ? (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"]
                : (Brush)Application.Current.Resources["SystemFillColorCriticalBrush"];

            ResultDialog.XamlRoot = this.Content.XamlRoot;
            await ResultDialog.ShowAsync();
        }
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var dataPackage = new DataPackage();
            dataPackage.SetText(temp);
            Clipboard.SetContent(dataPackage);
        }
    }
}