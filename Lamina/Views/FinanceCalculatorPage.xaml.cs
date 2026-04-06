using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation; // Add this using statement

namespace Lamina.Views;

public sealed partial class FinanceCalculatorPage : Page
{
    public FinanceCalculatorPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        // Load the Equilateral Triangle page by default
        ContentFrame.Navigate(typeof(SIPage));
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string shape = selectedItem.Content.ToString();

            if (ContentFrame == null)
            {
                System.Diagnostics.Debug.WriteLine("ContentFrame is null.");
                return;
            }

            switch (shape)
            {
                case "Simple Interest":
                    ContentFrame.Navigate(typeof(SIPage));
                    break;
                case "Compound Interest":
                    ContentFrame.Navigate(typeof(CIPage));
                    break;
                case "Recurring Deposit":
                    ContentFrame.Navigate(typeof(RDPage));
                    break;
            }
        }
    }
}