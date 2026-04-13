using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class CSurfaceAreaPage : Page
    {
        public CSurfaceAreaPage()
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
                // Formulae
                case "Cylinder":
                    FormulaInfoBar.Message = "CSA = 2πrh";
                    SetInputs("Radius (r)", "Height (h)");
                    break;
                case "Cone":
                    FormulaInfoBar.Message = "CSA = πrl";
                    SetInputs("Radius (r)", "Slant Height (l)");
                    break;
                case "Sphere":
                    FormulaInfoBar.Message = "CSA = 4πr²";
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

            // Reset colors to default theme brushes for successful calculations
            ResultLabel.Foreground = (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];
            ResultValueText.Foreground = (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"];

            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double a = InputA.Value;
            double b = InputB.Value;
            double csa = 0;

            if (double.IsNaN(a) || (InputB.Visibility == Visibility.Visible && double.IsNaN(b)))
            {
                ShowError("Error:", "Invalid inputs.");
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                // MATH TIME! (Don't worry, it's just basic geometry formulas)
                switch (shape)
                {
                    case "Cylinder": csa = 2 * Math.PI * a * b; break;
                    case "Cone": csa = Math.PI * a * b; break;
                    case "Sphere": csa = 4 * Math.PI * Math.Pow(a, 2); break;
                }

                if (double.IsNaN(csa) || double.IsInfinity(csa)) throw new Exception();

                ResultLabel.Text = "C. Surface Area =";
                ResultValueText.Text = csa.ToString("N2");
            }
            catch
            {
                ShowError("Error:", "Calculation error.");
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