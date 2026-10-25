using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lamina.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Parlot.Fluent;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        try
        {
            // Our centralized update endpoint!
            string jsonUrl = "https://chill-astro.github.io/assets/data/repo-data.json";
            // Local Server
            //string jsonUrl = "http://localhost:8000/assets/data/repo-data.json";

            string response = await _httpClient.GetStringAsync(jsonUrl);

            using JsonDocument document = JsonDocument.Parse(response);

            JsonElement lamina = document.RootElement.GetProperty("lamina");

            string latestTag = lamina.GetProperty("tag").GetString()
                ?? throw new Exception("Missing version tag. (×_×)");

            string setupUrl = lamina.GetProperty("downloads")
                .GetProperty("setup").GetString()
                ?? throw new Exception("Missing setup URL. (×_×)");

            // Only the universal Setup.exe needs a SHA-256 checksum.
            // Fail closed: never install when the hash is absent or malformed.
            string expectedSetupHash = lamina.GetProperty("sha256")
                .GetProperty("setup").GetString()
                ?? throw new InvalidDataException("Missing Setup.exe SHA-256 checksum.");

            if (expectedSetupHash.Length != 64 ||
                !System.Text.RegularExpressions.Regex.IsMatch(
                    expectedSetupHash, @"\A[0-9a-fA-F]{64}\z"))
                throw new InvalidDataException("Invalid Setup.exe SHA-256 checksum.");

            string cleanVersion = latestTag.Trim().TrimStart('v', 'V');

            if (!Version.TryParse(cleanVersion, out Version? latestVersion))
            {
                await DisplayDialog(
                    "Update Check",
                    "The Server Returned an Invalid Version Number! ¯\\_(ツ)_ /¯"
                );
                return;
            }

            Version currentVersion = Version.Parse(CurrentAppVersion);

            if (latestVersion > currentVersion)
            {
                // Make sure the installer URL points to our GitHub releases.
                if (!Uri.TryCreate(setupUrl, UriKind.Absolute, out Uri? uri)
                    || uri.Scheme != Uri.UriSchemeHttps
                    || uri.Host != "github.com"
                    || !uri.AbsolutePath.StartsWith(
                        "/Chill-Astro/Lamina-Calculator/releases/download/",
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Invalid installer URL.");
                }

                var dialog = new ContentDialog
                {
                    Title = "Update Available!",

                    Content =
                        "Lamina ✦ got a NEW UPDATE! (✿◡‿◡)\n\n" +
                        $"App Version = v{currentVersion}\n" +
                        $"Latest Version = v{latestVersion}\n\n" +
                        "Hit Update to Continue! ヾ(^▽^*)))",

                    CloseButtonText = "Close",
                    PrimaryButtonText = "Update",

                    PrimaryButtonStyle =
                        (Style)Application.Current.Resources["AccentButtonStyle"],

                    DefaultButton = ContentDialogButton.Primary,
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };

                ContentDialogResult result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    await DownloadAndInstallUpdate(setupUrl, latestTag, expectedSetupHash);
                }
            }
            else if (latestVersion < currentVersion)
            {
                await DisplayDialog(
                    "Update Check",
                    $"This is a DEV. BUILD Lamina ✦ ! (。_。)\n\n" +
                    $"App Version = v{currentVersion}\n" +
                    $"Latest Version = v{latestVersion}"
                );
            }
            else
            {
                await DisplayDialog(
                    "Update Check",
                    "Lamina ✦ is UP TO DATE! ヾ(^▽^*)))"
                );
            }
        }
        catch (Exception ex)
        {
            await DisplayDialog(
                "Update Check",
                "No Internet Connection! (×_×)"
            );
        }
        finally
        {
            _isCheckingUpdates = false;
        }
    }

    private async Task DownloadAndInstallUpdate(string setupUrl, string version, string expectedSetupHash)
    {
        string? downloadPath = null;
        bool completed = false;
        bool launched = false;

        using var cancellation = new CancellationTokenSource();

        var progressBar = new ProgressBar
        {
            Minimum = 0,
            Maximum = 100,
            Value = 0,
            IsIndeterminate = true
        };

        var statusText = new TextBlock
        {
            Text = "Connecting to the download server...",
            TextWrapping = TextWrapping.Wrap
        };

        var progressText = new TextBlock
        {
            Text = "Preparing download...",
            TextWrapping = TextWrapping.Wrap
        };

        var panel = new StackPanel { Spacing = 12 };
        panel.Children.Add(statusText);
        panel.Children.Add(progressBar);
        panel.Children.Add(progressText);

        var progressDialog = new ContentDialog
        {
            Title = "Downloading Update",
            Content = panel,
            CloseButtonText = "Cancel",
            CloseButtonStyle = (Style)Application.Current.Resources["AccentButtonStyle"],
            XamlRoot = App.MainWindow.Content.XamlRoot
        };

        progressDialog.CloseButtonClick += (_, _) => cancellation.Cancel();

        // Start showing the dialog without waiting for it to close.
        // Downloading continues asynchronously while the dialog is visible.
        Task<ContentDialogResult> dialogTask = progressDialog.ShowAsync().AsTask();

        try
        {
            string downloadsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads");
            Directory.CreateDirectory(downloadsFolder);

            string safeVersion = version.Trim().TrimStart('v', 'V');
            if (!Version.TryParse(safeVersion, out _))
                throw new InvalidOperationException("Invalid Installer Version. ¯\\_(ツ)_/¯");

            downloadPath = Path.Combine(downloadsFolder,
                $"Lamina_{safeVersion}_Update.exe");

            using HttpResponseMessage response = await _httpClient.GetAsync(
                setupUrl,
                HttpCompletionOption.ResponseHeadersRead,
                cancellation.Token);
            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;
            progressBar.IsIndeterminate = !totalBytes.HasValue || totalBytes.Value <= 0;
            statusText.Text = "Downloading setup...";

            await using Stream source = await response.Content.ReadAsStreamAsync(
                cancellation.Token);
            await using (FileStream destination = new FileStream(
                downloadPath, FileMode.Create, FileAccess.Write,
                FileShare.None, 81920, useAsync: true))
            {
                byte[] buffer = new byte[81920];
                long downloadedBytes = 0;
                int bytesRead;

                while ((bytesRead = await source.ReadAsync(
                    buffer.AsMemory(), cancellation.Token)) > 0)
                {
                    await destination.WriteAsync(
                        buffer.AsMemory(0, bytesRead), cancellation.Token);
                    downloadedBytes += bytesRead;

                    double downloadedMB = downloadedBytes / 1048576.0;
                    if (totalBytes is > 0)
                    {
                        double percent = Math.Min(100,
                            downloadedBytes * 100.0 / totalBytes.Value);
                        progressBar.Value = percent;
                        progressText.Text =
                            $"{percent:F0}%  •  {downloadedMB:F1} MB / " +
                            $"{totalBytes.Value / 1048576.0:F1} MB";
                    }
                    else
                    {
                        progressText.Text = $"{downloadedMB:F1} MB downloaded";
                    }
                }

                await destination.FlushAsync(cancellation.Token);

                if (totalBytes.HasValue && downloadedBytes != totalBytes.Value)
                    throw new IOException("The download was incomplete.");
            }

            cancellation.Token.ThrowIfCancellationRequested();
            statusText.Text = "Checking installer...";

            // This checks the EXE header only, not its authenticity.
            await using (FileStream file = File.OpenRead(downloadPath))
            {
                byte[] header = new byte[2];
                int count = await file.ReadAsync(header.AsMemory(), cancellation.Token);
                if (count != 2 || header[0] != 'M' || header[1] != 'Z')
                    throw new InvalidDataException("The Downloaded File isn't an EXE. (×_×)");
            }

            cancellation.Token.ThrowIfCancellationRequested();
            statusText.Text = "Verifying SHA-256 checksum...";

            byte[] actualHash;
            await using (FileStream hashStream = File.OpenRead(downloadPath))
            {
                actualHash = await SHA256.HashDataAsync(hashStream, cancellation.Token);
            }

            byte[] expectedHash = Convert.FromHexString(expectedSetupHash);
            if (!CryptographicOperations.FixedTimeEquals(actualHash, expectedHash))
                throw new InvalidDataException(
                    "SHA-256 Verification Failed. (×_×)");

            cancellation.Token.ThrowIfCancellationRequested();
            statusText.Text = "Launching setup...";

            Process? process = Process.Start(new ProcessStartInfo
            {
                FileName = downloadPath,
                WorkingDirectory = downloadsFolder,
                UseShellExecute = true
            });
            if (process == null)
                throw new InvalidOperationException("Couldn't Launch Setup. (×_×)");

            launched = true;
            completed = true;
        }
        catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
        {
            // User cancelled. Keep Lamina open.
        }
        catch (Exception ex)
        {
            statusText.Text = $"{ex.Message}";
        }
        finally
        {
            if (!completed && downloadPath != null)
            {
                try { File.Delete(downloadPath); }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }

            // Close progress dialog before displaying another dialog or exiting.
            progressDialog.Hide();
            await dialogTask;
        }

        if (launched)
        {
            Application.Current.Exit();
        }
        else if (!cancellation.IsCancellationRequested &&
                 statusText.Text.EndsWith("(×_×)", StringComparison.Ordinal))
        {
            await DisplayDialog("Update Failed", statusText.Text);
        }
    }

    private async Task DisplayDialog(string title, string content)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = title == "Update Failed" ? content : "Searching for Latest Release.... ♪(´▽｀)\n\n" + content,
            CloseButtonText = "Close",
            CloseButtonStyle =
                (Style)Application.Current.Resources["AccentButtonStyle"],
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
