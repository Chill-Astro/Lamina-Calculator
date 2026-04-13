using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
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

        private async void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;
            
            ResultLabel.Foreground = (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];
            ResultValueText.Foreground = (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"];
            CopyButton.Visibility = Visibility.Visible;

            double a = SideA.Value;
            double b = SideB.Value;
            double c = SideC.Value;

            // Validation for Missing / Zero / Negative ( Basically not Joking with Shapes )
            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c) || a <= 0 || b <= 0 || c <= 0)
            {
                ShowError("Error :", "Invalid Side Lengths");
                await ResultDialog.ShowAsync();
                return;
            }

            // Triangle Inequality Check ( Sum of any two sides must be greater than the third )
            if (a + b <= c || a + c <= b || b + c <= a)
            {
                ShowError("Invalid Geometry :", "Invalid Triangle");
                await ResultDialog.ShowAsync();
                return;
            }

            // MATHING TIME!
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
                ShowError("Error :", "Calculation Failed");
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