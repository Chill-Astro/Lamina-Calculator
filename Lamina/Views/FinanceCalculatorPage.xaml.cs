using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class FinanceCalculatorPage : Page
    {
        public FinanceCalculatorPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) => { ComboBox.SelectedIndex = 0; UpdateUI(); };
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateUI();

        private void UpdateUI()
        {
            if (ComboBox == null || InputA == null) return;

            InputA.Visibility = InputB.Visibility = InputC.Visibility = InputD.Visibility = Visibility.Collapsed;
            string type = (ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            switch (type)
            {
                // Formulae
                case "Simple Interest":
                    FormulaInfoBar.Message = "I = (P × R × T) / 100";
                    SetInputs("Principal (P)", "Rate % (R)", "Years (T)");
                    break;
                case "Compound Interest":
                    FormulaInfoBar.Message = "A = P(1 + r/n)^nt";
                    SetInputs("Principal (P)", "Rate % (r)", "Years (t)", "Compounding periods per year (n)");
                    InputD.Value = 12; // Default to monthly compounding
                    break;
                case "Recurring Deposit":
                    FormulaInfoBar.Message = "I = [P × n(n+1) / 24] × r/100";
                    SetInputs("Monthly Deposit (P)", "Rate % (r)", "Total Months (n)");
                    break;
            }
        }

        private void SetInputs(string a, string b, string c, string d = null)
        {
            InputA.Header = a; InputA.Visibility = Visibility.Visible;
            InputB.Header = b; InputB.Visibility = Visibility.Visible;
            InputC.Header = c; InputC.Visibility = Visibility.Visible;
            if (d != null) { InputD.Header = d; InputD.Visibility = Visibility.Visible; }
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content.XamlRoot == null) return;
            ResultDialog.XamlRoot = this.Content.XamlRoot;

            // Reset colors to default
            ResultLabel.Foreground = (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];
            ResultValueText.Foreground = (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"];

            string type = (ComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double p = InputA.Value;
            double r = InputB.Value;
            double t = InputC.Value;
            double n = InputD.Value;

            double interest = 0, totalAmount = 0;
            
            if (double.IsNaN(p) || double.IsNaN(r) || double.IsNaN(t))
            {
                ShowError("Error :", "Please Fill All Fields");
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                switch (type)
                {
                    // MATH TIME (I hope you remember all these formulae from school :D )
                    case "Simple Interest":
                        interest = (p * r * t) / 100.0;
                        totalAmount = p + interest;
                        ResultLabel.Text = $"Total Maturity: {totalAmount:N2}";
                        ResultValueText.Text = interest.ToString("N2");
                        break;

                    case "Compound Interest":
                        if (double.IsNaN(n) || n <= 0) n = 1;
                        totalAmount = p * Math.Pow((1 + (r / 100.0) / n), n * t);
                        interest = totalAmount - p;
                        ResultLabel.Text = $"Interest Earned: {interest:N2}";
                        ResultValueText.Text = totalAmount.ToString("N2");
                        break;

                    case "Recurring Deposit":
                        interest = (p * t * (t + 1) / 24.0) * (r / 100.0);
                        totalAmount = (p * t) + interest;
                        ResultLabel.Text = $"Total Maturity: {totalAmount:N2}";
                        ResultValueText.Text = interest.ToString("N2");
                        break;
                }
            }
            catch
            {
                ShowError("Error :", "Calculation failed");
            }

            await ResultDialog.ShowAsync();
        }
        // Red has highest Wavelength so it's the color chosen for errors. ( I hope you remember Physics :D )
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