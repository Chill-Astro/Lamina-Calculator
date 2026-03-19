using Lamina.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Lamina.Views;

public sealed partial class DateCalculatorPage : Page
{
    public DateCalculatorViewModel ViewModel
    {
        get;
    }

    public DateCalculatorPage()
    {
        ViewModel = App.GetService<DateCalculatorViewModel>();
        InitializeComponent();
    }
}
