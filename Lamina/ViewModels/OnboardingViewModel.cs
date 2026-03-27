using CommunityToolkit.Mvvm.ComponentModel;
using Lamina.Contracts.Services;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace Lamina.ViewModels;

public partial class OnboardingViewModel : ObservableRecipient
{
    private readonly IMicaService _micaService;
    private readonly ILocalSettingsService _localSettingsService;

    [ObservableProperty]
    private int _selectedBackdropIndex;

    public OnboardingViewModel(IMicaService micaService, ILocalSettingsService localSettingsService)
    {
        _micaService = micaService;
        _localSettingsService = localSettingsService;

        // Sync initial backdrop index
        Task.Run(async () => {
            var savedIndex = await _localSettingsService.ReadSettingAsync<int?>("AppBackdropIndex") ?? 0;
            App.MainWindow.DispatcherQueue.TryEnqueue(() => { SelectedBackdropIndex = savedIndex; });
        });
    }

    partial void OnSelectedBackdropIndexChanged(int value)
    {
        _micaService.SetBackdrop(value);
        _ = _micaService.SaveMicaSettingAsync(value);
    }

    // Property for the ToggleSwitch in Slide 2
    public bool IsSplashEnabled
    {
        get => Windows.Storage.ApplicationData.Current.LocalSettings.Values["ShowSplash"] as bool? ?? true;
        set
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["ShowSplash"] = value;
            OnPropertyChanged(nameof(IsSplashEnabled));
        }
    }

    public int SelectedThemeIndex
    {
        get => (int)App.GetService<IThemeSelectorService>().Theme;
        set
        {
            _ = App.GetService<IThemeSelectorService>().SetThemeAsync((ElementTheme)value);
            OnPropertyChanged(nameof(SelectedThemeIndex));
        }
    }
}