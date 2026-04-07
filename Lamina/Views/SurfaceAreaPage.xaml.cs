using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views
{
    public sealed partial class SurfaceAreaPage : Page
    {
        public SurfaceAreaPage()
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
                case "Cube":
                    FormulaInfoBar.Message = "SA = 6a²";
                    SetInputs("Side (a)");
                    break;
                case "Cuboid":
                    FormulaInfoBar.Message = "SA = 2(lw + wh + hl)";
                    SetInputs("Length (l)", "Width (w)", "Height (h)");
                    break;
                case "Cylinder":
                    FormulaInfoBar.Message = "SA = 2πr(r + h)";
                    SetInputs("Radius (r)", "Height (h)");
                    break;
                case "Cone":
                    FormulaInfoBar.Message = "SA = πr(r + l)";
                    SetInputs("Radius (r)", "Slant Height (l)");
                    break;
                case "Sphere":
                    FormulaInfoBar.Message = "SA = 4πr²";
                    SetInputs("Radius (r)");
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

            string shape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
            double a = InputA.Value;
            double b = InputB.Value;
            double c = InputC.Value;
            double surfaceArea = 0;

            if (double.IsNaN(a) ||
               (InputB.Visibility == Visibility.Visible && double.IsNaN(b)) ||
               (InputC.Visibility == Visibility.Visible && double.IsNaN(c)))
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Please enter valid numbers.";
                await ResultDialog.ShowAsync();
                return;
            }

            try
            {
                switch (shape)
                {
                    case "Cube":
                        surfaceArea = 6 * Math.Pow(a, 2);
                        break;
                    case "Cuboid":
                        // a=l, b=w, c=h
                        surfaceArea = 2 * (a * b + b * c + c * a);
                        break;
                    case "Cylinder":
                        // a=r, b=h
                        surfaceArea = 2 * Math.PI * a * (a + b);
                        break;
                    case "Cone":
                        // a=r, b=l (slant height)
                        surfaceArea = Math.PI * a * (a + b);
                        break;
                    case "Sphere":
                        surfaceArea = 4 * Math.PI * Math.Pow(a, 2);
                        break;
                }

                ResultLabel.Text = "Surface Area =";
                ResultValueText.Text = surfaceArea.ToString("N2");
            }
            catch
            {
                ResultLabel.Text = "Error:";
                ResultValueText.Text = "Invalid dimensions.";
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