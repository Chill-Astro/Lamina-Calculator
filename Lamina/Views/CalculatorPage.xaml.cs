using System;
using System.Linq;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Core;
using Lamina.ViewModels;

namespace Lamina.Views;

public sealed partial class CalculatorPage : Page
{
    private bool _isDialogOpen;
    public CalculatorViewModel ViewModel { get; }

    public CalculatorPage()
    {
        ViewModel = App.GetService<CalculatorViewModel>();
        InitializeComponent();

        // Focus the page so keyboard input works immediately
        this.Loaded += (s, e) => this.Focus(FocusState.Programmatic);
    }

    private async void OpenHistoryDialog()
    {
        if (_isDialogOpen) return;
        _isDialogOpen = true;

        try
        {
            var historyPage = new HistoryPage();
            historyPage.SetHistory(ViewModel.CalculationHistory.ToList());
            historyPage.HorizontalAlignment = HorizontalAlignment.Stretch;
            historyPage.VerticalAlignment = VerticalAlignment.Stretch;
            var dialog = new ContentDialog
            {
                Content = historyPage,
                CloseButtonText = "Close",
                XamlRoot = this.XamlRoot,
                RequestedTheme = this.RequestedTheme,
                MaxWidth = 400
            };

            await dialog.ShowAsync();
        }
        finally
        {
            _isDialogOpen = false;
        }
    }

    private void CalculatorPage_Loaded(object sender, RoutedEventArgs e)
    {
        this.Focus(FocusState.Programmatic);
    }

    /// <summary>
    /// Animates the button to provide visual feedback for keyboard presses.
    /// </summary>
    private async void AnimateClick(Button button)
    {
        if (button == null) return;

        // Trigger the 'Pressed' visual state from your XAML Style
        VisualStateManager.GoToState(button, "Pressed", true);

        // Brief delay to make the animation visible to the user
        await System.Threading.Tasks.Task.Delay(100);

        // Return to the 'Normal' state
        VisualStateManager.GoToState(button, "Normal", true);
    }

    private void CalculatorPage_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
    {
        // CoreVirtualKeyStates is the correct WinUI 3 enum for checking modifier keys
        var shift = InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift).HasFlag(CoreVirtualKeyStates.Down);
        var ctrl = InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control).HasFlag(CoreVirtualKeyStates.Down);

        bool handled = true;
        Button targetButton = null;

        // 1. Handle Numbers (Main Row and Numpad)
        if (e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9 && !shift)
        {
            string num = (e.Key - VirtualKey.Number0).ToString();
            ViewModel.InputNumberCommand.Execute(num);
            targetButton = this.FindName("Btn" + num) as Button;
        }
        else if (e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.NumberPad9)
        {
            string num = (e.Key - VirtualKey.NumberPad0).ToString();
            ViewModel.InputNumberCommand.Execute(num);
            targetButton = this.FindName("Btn" + num) as Button;
        }
        // 2. Handle Operators and Actions
        else
        {
            switch (e.Key)
            {
                case VirtualKey.Add:
                case (VirtualKey)187 when shift: // '+' key
                    ViewModel.SetOperatorCommand.Execute("+");
                    targetButton = BtnAdd;
                    break;

                case VirtualKey.Subtract:
                case (VirtualKey)189: // '-' key
                    ViewModel.SetOperatorCommand.Execute("-");
                    targetButton = BtnSubtract;
                    break;

                case VirtualKey.Multiply:
                case VirtualKey.Number8 when shift: // '*' key
                    ViewModel.SetOperatorCommand.Execute("×");
                    targetButton = BtnMultiply;
                    break;

                case VirtualKey.Divide:
                case (VirtualKey)191: // '/' key
                    ViewModel.SetOperatorCommand.Execute("÷");
                    targetButton = BtnDivide;
                    break;

                case VirtualKey.Enter:
                case (VirtualKey)187 when !shift: // '=' key
                    ViewModel.CalculateCommand.Execute(null);
                    targetButton = BtnEqual;
                    break;

                case VirtualKey.Back:
                    ViewModel.BackspaceCommand.Execute(null);
                    targetButton = BtnBackspace;
                    break;

                case VirtualKey.Escape:
                case VirtualKey.C:
                    ViewModel.ClearAllCommand.Execute(null);
                    targetButton = BtnC;
                    break;

                case VirtualKey.Decimal:
                case (VirtualKey)190: // '.' key
                    ViewModel.InputDecimalCommand.Execute(null);
                    targetButton = BtnDecimal;
                    break;

                case VirtualKey.H when ctrl:
                    OpenHistoryDialog();
                    // Maps to the history button in the top right
                    targetButton = this.FindName("HistoryButton") as Button;
                    break;

                default:
                    handled = false;
                    break;
            }
        }

        // 3. Finalize Event
        if (handled)
        {
            e.Handled = true;
            if (targetButton != null)
            {
                AnimateClick(targetButton);
            }
        }
    }

    private void HistoryButton_Click(object sender, RoutedEventArgs e) => OpenHistoryDialog();
}