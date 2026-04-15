using System;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lamina.Contracts.Services;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel;
using Microsoft.UI.Xaml.Controls;

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

    [ObservableProperty] private string _appVersionText;
    [ObservableProperty] private int _selectedThemeIndex;
    [ObservableProperty] private int _selectedBackdropIndex;

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IMicaService micaService, ILocalSettingsService localSettingsService)
    {
        // All the Glassy UI Settings Stuff.
        _themeSelectorService = themeSelectorService;
        _micaService = micaService;
        _localSettingsService = localSettingsService;
        
        // Index for iLikeToFryMyEyes (Light) and iAmTheLoanWolf (Dark) theme or whatever Binbows 11 says.
        _selectedThemeIndex = (int)_themeSelectorService.Theme;

        Task.Run(async () => {
            var savedIndex = await _localSettingsService.ReadSettingAsync<int?>("AppBackdropIndex") ?? 0;
            App.MainWindow.DispatcherQueue.TryEnqueue(() => { SelectedBackdropIndex = savedIndex; });
        });

        AppVersionText = $" v{CurrentAppVersion}";
    }

    partial void OnSelectedThemeIndexChanged(int value) => _themeSelectorService.SetThemeAsync((ElementTheme)value);

    partial void OnSelectedBackdropIndexChanged(int value)
    {
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
                    // Bro took so long to go to 2026 💀
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
                    message = $"Woohooo! ヽ(✿ﾟ▽ﾟ)ノ \n\nThere's a New Release of Lamina ✦ ! 🎉\n\nApp Version = v{currentVersion}\nLatest Version = v{latestVersion}\n\nHit Update to Download! ヾ(^▽^*)))";

                    await DisplayDialog("Update Check", message); // Use the Helper if New Version.
                    
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Calculator/releases/latest"));

                    _isCheckingUpdates = false;
                    return;
                }
                else if (latestVersion < currentVersion)
                {
                    message = $"That looks a bit Unstable! (。_。) \n\nThis is a DEV. BUILD Lamina ✦ ! ⚠️\n\nApp Version = v{currentVersion}\nLatest Version = v{latestVersion}"; // If using a Buggy Build.
                }
                else
                {
                    message = "Woohooo! ヾ(^▽^*))) \n\nLamina ✦ is UP TO DATE! 🎉"; // If using the Latest and Greatest!
                }
            }
            else
            {
                message = "Is the Server confused or me? ¯\\_(ツ)_/¯ \n\nThe Server Returned an Invalid Version Number! ❌"; // If the Server is like 67 Kid.
            }
        }
        catch
        {
            message = "Oh Snap! (╯°□°）╯︵ ┻━┻ \n\nPlease Verify your Internet Connection! ❌"; // If ur WiFi is as bad as your Reels Feed.
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

    public bool IsSplashEnabled // No for Reel Scrollers and Serious Mathematicians, but Yes for UI Design Lovers (like me).
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