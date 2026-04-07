using System;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lamina.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Lamina.ViewModels;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IMicaService _micaService;
    private readonly ILocalSettingsService _localSettingsService;
    private static readonly HttpClient _httpClient = new();

    // UPDATE THIS EVERYTIME YOU DUMMY!
    private const string CurrentAppVersion = "11.26100.14.0";

    private bool _isCheckingUpdates;

    [ObservableProperty] private string _appVersionText;
    [ObservableProperty] private int _selectedThemeIndex;
    [ObservableProperty] private int _selectedBackdropIndex;

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IMicaService micaService, ILocalSettingsService localSettingsService)
    {
        _themeSelectorService = themeSelectorService;
        _micaService = micaService;
        _localSettingsService = localSettingsService;

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
            Title = "MIT License",
            CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],
            Content = new ScrollViewer
            {
                MaxHeight = 200,
                Content = new TextBlock
                {
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
            // Adding a timestamp to the URL prevents GitHub/Windows from caching an old version of the Gist
            string gistUrl = $"https://gist.githubusercontent.com/Chill-Astro/ac961f2e3f9a2de6b358de9be9a2bfc1/raw/LMNA_V?t={DateTime.Now.Ticks}";
            string response = await _httpClient.GetStringAsync(gistUrl);

            string cleanResponse = response.Trim().ToLower().Replace("v", "");

            if (Version.TryParse(cleanResponse, out var latestVersion))
            {
                Version currentVersion = Version.Parse(CurrentAppVersion);

                if (latestVersion > currentVersion)
                {
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Calculator/releases"));
                    _isCheckingUpdates = false;
                    return;
                }
                else if (latestVersion < currentVersion)
                {
                    message = $"Lamina ✦ DEV. BUILD! ⚠️\nApp Version = v{currentVersion}\nLatest Version = v{latestVersion}";
                }
                else
                {
                    message = "Lamina ✦ is UP TO DATE! 🎉";
                }
            }
            else
            {
                message = "The Update Server returned an invalid version format. ⚠️";
            }
        }
        catch
        {
            message = "Please Verify your Internet Connection! ❌";
        }

        if (!string.IsNullOrEmpty(message))
        {
            var updateDialog = new ContentDialog
            {
                Title = "Update Check",
                Content = message,
                CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],
                CloseButtonText = "Close",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await updateDialog.ShowAsync();
        }

        _isCheckingUpdates = false;
    }

    public bool IsSplashEnabled
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