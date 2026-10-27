using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Lamina.Views;

public sealed partial class QuadEqnPage : Page
{
    static string temp = "";

    public QuadEqnPage()
    {
        InitializeComponent();
    }

    private async void CalculateRootsButton_Click(object sender, RoutedEventArgs e)
    {
        double a = Number1.Value;
        double b = Number2.Value;
        double c = Number3.Value;

        // Check for Missing Input
        if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
        {
            temp = "";
            await ShowResultPopup("Incomplete Input", "Please Fill All Fields", false);
            return;
        }

        // Handle linear equation (a = 0)
        if (a == 0)
        {
            if (b == 0)
            {
                temp = "";
                await ShowResultPopup("Invalid Equation", c == 0 ? "Infinite Solutions (0=0)" : "No Solution", false);
            }
            else
            {
                double root = -c / b;
                temp = root.ToString("0.##");
                await ShowResultPopup("Linear Result", $"x = {temp}", true);
            }
            return;
        }
        
        // Checking Which Kind of Roots your Inputs Produce
        double discriminant = (b * b) - (4 * a * c);
        // MATHING TIME! Btw this Formula is Indian! 
        if (discriminant > 0)
        {
            double r1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double r2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            temp = $"{r1:0.##}, {r2:0.##}";
            await ShowResultPopup("Two Real Roots", temp, true);
        }
        else if (discriminant == 0)
        {
            double r = -b / (2 * a);
            temp = r.ToString("0.##");
            await ShowResultPopup("One Real Root", $"x = {temp}", true);
        }
        else
        {
            double realPart = -b / (2 * a);
            double imaginaryPart = Math.Sqrt(-discriminant) / (2 * Math.Abs(a));
            temp = $"{realPart:0.##} + {imaginaryPart:0.##}i, {realPart:0.##} - {imaginaryPart:0.##}i";
            await ShowResultPopup("Two Imaginary Roots", temp, true, true);
        }
    }

    private async System.Threading.Tasks.Task ShowResultPopup(string contextText, string actualValue, bool isSuccess, bool isImaginary = false)
    {      
        ResultLabel.Text = contextText;    
        ResultValueText.Text = actualValue; 

        // Visuals
        CopyButton.Visibility = isSuccess ? Visibility.Visible : Visibility.Collapsed;
        ResultLabel.Foreground = isImaginary
            ? (Brush)Application.Current.Resources["SystemFillColorCriticalBrush"]
            : (Brush)Application.Current.Resources["TextFillColorSecondaryBrush"];

        ResultValueText.Foreground = isSuccess
            ? (Brush)Application.Current.Resources["TextFillColorPrimaryBrush"]
            : (Brush)Application.Current.Resources["SystemFillColorCriticalBrush"];

        ResultDialog.XamlRoot = this.Content.XamlRoot;
        await ResultDialog.ShowAsync();
    }

    private void CopyButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(temp)) return;
        var dataPackage = new DataPackage();
        dataPackage.SetText(temp);
        Clipboard.SetContent(dataPackage);
    }
}
