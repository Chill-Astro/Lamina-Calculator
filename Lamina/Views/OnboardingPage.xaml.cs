using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using WinRT.Interop;
using System;
using System.Threading.Tasks;
using Lamina.ViewModels;

namespace Lamina.Views;

public sealed partial class OnboardingPage : Page
{
    public OnboardingViewModel ViewModel { get; }
    private int _currentIndex = 0;

    public OnboardingPage()
    {
        ViewModel = App.GetService<OnboardingViewModel>();
        this.InitializeComponent();
        this.Loaded += OnboardingPage_Loaded;
    }

    private void OnboardingPage_Loaded(object sender, RoutedEventArgs e)
    {
        // Transparent TitleBar Buttons logic
        var hWnd = WindowNative.GetWindowHandle(App.MainWindow);
        var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

        if (appWindow.TitleBar is not null)
        {
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }

    private void Next_Click(object sender, RoutedEventArgs e)
    {
        if (_currentIndex == 0) AnimateTo(Slide1, Slide2, 1);
        else if (_currentIndex == 1) AnimateTo(Slide2, Slide3, 2);
    }

    private void AnimateTo(StackPanel current, StackPanel next, int index)
    {
        current.Visibility = Visibility.Collapsed;
        next.Visibility = Visibility.Visible;
        _currentIndex = index;
        OnboardingPipsPager.SelectedPageIndex = _currentIndex;

        var sb = new Storyboard();
        var slide = new DoubleAnimation { From = 300, To = 0, Duration = new Duration(TimeSpan.FromMilliseconds(400)) };
        slide.EasingFunction = new ExponentialEase { Exponent = 7, EasingMode = EasingMode.EaseOut };

        if (next.RenderTransform is not TranslateTransform) next.RenderTransform = new TranslateTransform();
        Storyboard.SetTarget(slide, next);
        Storyboard.SetTargetProperty(slide, "(UIElement.RenderTransform).(TranslateTransform.X)");
        sb.Children.Add(slide);
        sb.Begin();
    }

    private async void Finish_Click(object sender, RoutedEventArgs e)
    {
        // 1. Save state
        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        localSettings.Values["FirstLaunch"] = false;

        // 2. Fade out
        this.Opacity = 0;
        await System.Threading.Tasks.Task.Delay(250);

        // 3. Create ShellPage with its ViewModel
        var shellViewModel = App.GetService<ShellViewModel>();
        var shellPage = new ShellPage(shellViewModel);

        // 4. Set as Window Content
        App.MainWindow.Content = shellPage;

        // 5. CRITICAL: Force the window to update
        App.MainWindow.Activate();
    }
}