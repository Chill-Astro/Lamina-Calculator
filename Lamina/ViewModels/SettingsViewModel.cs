using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lamina.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Lamina.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IMicaService _micaService;
    private readonly ILocalSettingsService _localSettingsService;
    private static readonly HttpClient _httpClient = new();

    // Automatically gets App Version so I don't have to Increment EVERYTIME. Yaaaaaay! 🎉
    static PackageVersion pv = Package.Current.Id.Version;
    private readonly string CurrentAppVersion = $"{pv.Major}.{pv.Minor}.{pv.Build}.{pv.Revision}";    
    
    private bool _isCheckingUpdates; // No
    private int _tapCount = 0; // 3435 is My Favorite Number because, 3^3 + 4^4 + 3^3 + 5^5 = 3435! :)

    [ObservableProperty] private string _appVersionText;
    [ObservableProperty] private int _selectedThemeIndex;
    [ObservableProperty] private int _selectedBackdropIndex;
    [ObservableProperty] private bool _isBackdropCardDisabled;

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IMicaService micaService, ILocalSettingsService localSettingsService)
    {
        // All the Glassy UI Settings Stuff.
        _themeSelectorService = themeSelectorService;
        _micaService = micaService;
        _localSettingsService = localSettingsService;
        
        // Index for iLikeToFryMyEyes (Light) and Dark theme or whatever Binbows 11 says.
        _selectedThemeIndex = (int)_themeSelectorService.Theme;

        // Check Windows version and handle Windows 10 restrictions
        bool isWindows10 = Services.WindowsVersionService.IsWindows10();
        IsBackdropCardDisabled = isWindows10;

        Task.Run(async () => {
            var savedIndex = await _localSettingsService.ReadSettingAsync<int?>("AppBackdropIndex") ?? 0;
            var finalIndex = isWindows10 ? 2 : savedIndex;
            App.MainWindow.DispatcherQueue.TryEnqueue(() => { 
                SelectedBackdropIndex = finalIndex;
                // Apply the backdrop immediately on UI thread
                _micaService.SetBackdrop(finalIndex);
            });
        });

        AppVersionText = $" v{CurrentAppVersion}";
    }

    partial void OnSelectedThemeIndexChanged(int value) => _themeSelectorService.SetThemeAsync((ElementTheme)value);

    partial void OnSelectedBackdropIndexChanged(int value)
    {
        // Prevent backdrop changes on Windows 10, but still apply Acrylic
        if (IsBackdropCardDisabled)
        {
            // Force back to Acrylic (index 2) and apply it
            SelectedBackdropIndex = 2;
            _micaService.SetBackdrop(2);
            _ = _micaService.SaveMicaSettingAsync(2);
            return;
        }
        
        _micaService.SetBackdrop(value);
        _ = _micaService.SaveMicaSettingAsync(value);
    }

    [RelayCommand]
    private async Task OpenRepo() => await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Calculator"));

    [RelayCommand]
    private async Task OpenProfile() => await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro"));

    [RelayCommand]
    private async Task ShowLicense()
    {
        var licenseDialog = new ContentDialog
        {
            Title = "MIT License", // Boring Legal Stuff if you are curious.
            CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],
            Content = new ScrollViewer
            {
                MaxHeight = 200,
                Content = new TextBlock
                {
                    // Bro took so long to Update his Licence! 💀
                    Text = "MIT License\n\nCopyright (c) 2026 Chill-Astro Software\n\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\n\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\n\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 13
                }
            },
            CloseButtonText = "Close",
            XamlRoot = App.MainWindow.Content.XamlRoot
        };
        await licenseDialog.ShowAsync();
    }

    [RelayCommand]
    public async Task VersionTap()
    {
        _tapCount++;
        if (_tapCount == 5)
        {
            _tapCount = 0; // Reset
            await ShowEasterEgg();
        }
    }
    
    [RelayCommand]
    private async Task ShowEasterEgg()
    {
        var eggDialog = new ContentDialog
        {
            Title = "✦ Easter Egg! ✦",
            CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],            
            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                MaxHeight = 300,
                Content = new StackPanel
                {
                    Spacing = 10,
                    Padding = new Thickness(0, 0, 10, 0), 
                    Children =
                {
                    new TextBlock
                    {
                        Text = "Here's a Cute Art! (✿◡‿◡)",
                        TextWrapping = TextWrapping.Wrap,
                        FontSize = 14,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center,
                        Opacity = 0.8
                    },
                    new Border
                    {
                        CornerRadius = new CornerRadius(5), 
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Child = new Image
                        {
                            Source = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage(new Uri("ms-appx:///Assets/EasterEgg.png")), // Meet Lamina ✦ !
                            MaxWidth = 300,
                            Stretch = Stretch.Uniform
                        }
                    }
                }
                }
            },
            CloseButtonText = "Cool!", // Pookie!
            XamlRoot = App.MainWindow.Content.XamlRoot
        };
        await eggDialog.ShowAsync();
    }

    [RelayCommand]
    private async Task CheckForUpdates()
    {
        if (_isCheckingUpdates) return;
        _isCheckingUpdates = true;

        string message = "";
        try
        {
            string gistUrl = $"https://gist.githubusercontent.com/Chill-Astro/ac961f2e3f9a2de6b358de9be9a2bfc1/raw/LMNA_V?t={DateTime.Now.Ticks}";
            string response = await _httpClient.GetStringAsync(gistUrl);
            string cleanResponse = response.Trim().ToLower().Replace("v", "");

            if (Version.TryParse(cleanResponse, out var latestVersion))
            {
                Version currentVersion = Version.Parse(CurrentAppVersion);

                if (latestVersion > currentVersion)
                {
                    message = $"Lamina ✦ got a NEW UPDATE! (✿◡‿◡)\n\nApp Version = v{currentVersion}\nLatest Version = v{latestVersion}\n\nHit Update to Download! ヾ(^▽^*)))";

                    await DisplayDialog("Update Check", message); // Use the Helper if New Version.
                    
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Calculator/releases/latest"));

                    _isCheckingUpdates = false;
                    return;
                }
                else if (latestVersion < currentVersion)
                {
                    message = $"This is a DEV. BUILD Lamina ✦ ! (。_。)\n\nApp Version = v{currentVersion}\nLatest Version = v{latestVersion}"; // If using a Buggy Build.
                }
                else
                {
                    message = "Lamina ✦ is UP TO DATE! ヾ(^▽^*)))"; // If using the Latest and Greatest!
                }
            }
            else
            {
                message = "The Server Returned an Invalid Version Number! ¯\\_(ツ)_/¯"; // If the Server is like 67 Kid.
            }
        }
        catch
        {
            message = "No Internet Connection! (×_×)"; // If ur WiFi is as bad as your Reels Feed.
        }

        // Show the dialog for everything EXCEPT the "New Release"
        if (!string.IsNullOrEmpty(message))
        {
            await DisplayDialog("Update Check", message);
        }

        _isCheckingUpdates = false;
    }  
    private async Task DisplayDialog(string title, string content)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = "Searching for Latest Release.... ♪(´▽｀)\n\n" + content,
            CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],
            CloseButtonText = content.Contains("New Release") ? "Update" : "Close",
            XamlRoot = App.MainWindow.Content.XamlRoot
        };
        await dialog.ShowAsync();
    }

    public bool IsSplashEnabled // Now you have the choice!
    {
        get
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            return localSettings.Values["ShowSplash"] as bool? ?? true;
        }
        set
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["ShowSplash"] = value;
            OnPropertyChanged(nameof(IsSplashEnabled));
        }
    }
}
