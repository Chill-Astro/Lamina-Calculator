using System;
using System.Threading.Tasks;

using Lamina.Activation;
using Lamina.Contracts.Services;
using Lamina.Core.Contracts.Services;
using Lamina.Core.Services;
using Lamina.Models;
using Lamina.Services;
using Lamina.ViewModels;
using Lamina.Views;

using LaunchActivatedEventArgs = Microsoft.UI.Xaml.LaunchActivatedEventArgs;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace Lamina
{
    public partial class App : Application
    {
        public IHost Host { get; }

        public static T GetService<T>() where T : class
        {
            if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
            {
                throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
            }
            return service;
        }

        public static WindowEx MainWindow { get; } = new MainWindow();

        public static UIElement? AppTitlebar { get; set; }

        public App()
        {
            InitializeComponent();

            Host = Microsoft.Extensions.Hosting.Host.
            CreateDefaultBuilder().
            UseContentRoot(AppContext.BaseDirectory).
            ConfigureServices((context, services) =>
            {
                // Activation Handlers
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Services
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddTransient<INavigationViewService, NavigationViewService>();
                services.AddSingleton<IMicaService, MicaService>();
                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<RDViewModel>();
                services.AddTransient<RDPage>();
                services.AddTransient<OnboardingViewModel>();
                services.AddTransient<OnboardingPage>();
                services.AddTransient<FinanceCalculatorViewModel>();
                services.AddTransient<FinanceCalculatorPage>();
                services.AddTransient<DateCalculatorViewModel>();
                services.AddTransient<DateCalculatorPage>();
                services.AddTransient<CurrencyViewModel>();
                services.AddTransient<CurrencyPage>();
                services.AddTransient<QuadEqnViewModel>();
                services.AddTransient<QuadEqnPage>();
                services.AddTransient<BaseConverterViewModel>();
                services.AddTransient<BaseConverterPage>();
                services.AddTransient<UnitConverterViewModel>();
                services.AddTransient<UnitConverterPage>();
                services.AddTransient<DateDifferenceViewModel>();
                services.AddTransient<DicountViewModel>();
                services.AddTransient<SphereCSAViewModel>();
                services.AddTransient<SphereCSAPage>();
                services.AddTransient<ConeCSAViewModel>();
                services.AddTransient<ConeCSAPage>();
                services.AddTransient<CylinderCSAViewModel>();
                services.AddTransient<CylinderCSAPage>();
                services.AddTransient<CSurfaceAreaViewModel>();
                services.AddTransient<CSurfaceAreaPage>();
                services.AddTransient<RoomAreaViewModel>();
                services.AddTransient<RoomAreaPage>();
                services.AddTransient<TriAreaViewModel>();
                services.AddTransient<TriAreaPage>();
                services.AddTransient<PrimeCheckViewModel>();
                services.AddTransient<CuboidDiagViewModel>();
                services.AddTransient<CuboidDiagPage>();
                services.AddTransient<CubeDiagViewModel>();
                services.AddTransient<CubeDiagPage>();
                services.AddTransient<RectDiagViewModel>();
                services.AddTransient<RectDiagPage>();
                services.AddTransient<SquareDiagViewModel>();
                services.AddTransient<SquareDiagPage>();
                services.AddTransient<SCirclePermViewModel>();
                services.AddTransient<SCirclePermPage>();

                services.AddTransient<CirclePermViewModel>();
                services.AddTransient<CirclePermPage>();
                services.AddTransient<RectPermViewModel>();
                services.AddTransient<RectPermPage>();
                services.AddTransient<SquarePermViewModel>();
                services.AddTransient<SquarePermPage>();
                services.AddTransient<ITPermViewModel>();
                services.AddTransient<ITPermPage>();
                services.AddTransient<ETPermViewModel>();
                services.AddTransient<ETPermPage>();
                services.AddTransient<PerimeterViewModel>();
                services.AddTransient<PerimeterPage>();
                services.AddTransient<DiagonalViewModel>();
                services.AddTransient<DiagonalPage>();
                services.AddTransient<SphereSAViewModel>();
                services.AddTransient<SphereSAPage>();
                services.AddTransient<ConeSAViewModel>();
                services.AddTransient<ConeSAPage>();
                services.AddTransient<CylinderSAViewModel>();
                services.AddTransient<CylinderSAPage>();
                services.AddTransient<CuboidSAViewModel>();
                services.AddTransient<CuboidSAPage>();
                services.AddTransient<CubeSAViewModel>();
                services.AddTransient<CubeSAPage>();
                services.AddTransient<SphereViewModel>();
                services.AddTransient<SpherePage>();
                services.AddTransient<ConeVolumeViewModel>();
                services.AddTransient<ConeVolumePage>();
                services.AddTransient<CylinderVolumeViewModel>();
                services.AddTransient<CylinderVolumePage>();
                services.AddTransient<CuboidVolumeViewModel>();
                services.AddTransient<CuboidVolumePage>();
                services.AddTransient<CubeVolumeViewModel>();
                services.AddTransient<CubeVolumePage>();
                services.AddTransient<SurfaceAreaViewModel>();
                services.AddTransient<SurfaceAreaPage>();
                services.AddTransient<VolumeViewModel>();
                services.AddTransient<VolumePage>();
                services.AddTransient<SCircleAreaViewModel>();
                services.AddTransient<SCircleAreaPage>();
                services.AddTransient<CircleAreaViewModel>();
                services.AddTransient<CircleAreaPage>();
                services.AddTransient<RhombusAreaViewModel>();
                services.AddTransient<RhombusAreaPage>();
                services.AddTransient<RectAreaViewModel>();
                services.AddTransient<RectAreaPage>();
                services.AddTransient<SquareAreaViewModel>();
                services.AddTransient<SquareAreaPage>();
                services.AddTransient<ITAreaViewModel>();
                services.AddTransient<ITAreaPage>();
                services.AddTransient<ETAreaViewModel>();
                services.AddTransient<ETAreaPage>();
                services.AddTransient<AreaViewModel>();
                services.AddTransient<AreaPage>();
                services.AddTransient<FactorialViewModel>();
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<CIViewModel>();
                services.AddTransient<CIPage>();
                services.AddTransient<SIViewModel>();
                services.AddTransient<SIPage>();
                services.AddTransient<HFViewModel>();
                services.AddTransient<HFPage>();
                services.AddTransient<CalculatorViewModel>();
                services.AddTransient<CalculatorPage>();
                services.AddTransient<ShellPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<SplashPage>();
                services.AddTransient<SplashViewModel>();

                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            }).
            Build();

            UnhandledException += App_UnhandledException;
        }

        protected async override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // 1. Setup Backdrop/Theme Resources
            var micaService = App.GetService<IMicaService>();
            await micaService.LoadMicaSettingAsync();

            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            bool showSplash = localSettings.Values["ShowSplash"] as bool? ?? true;
            bool isFirstLaunch = localSettings.Values["FirstLaunch"] as bool? ?? true;

            if (!showSplash)
            {
                await FinalizeNavigation(isFirstLaunch, args);
            }
            else
            {
                // Show Splash Flow
                var splashScreen = new SplashPage();
                MainWindow.Content = splashScreen;
                MainWindow.Activate();

                // Wait for splash visibility
                await Task.Delay(1300);

                // Play FadeOut if defined in SplashPage.xaml
                if (splashScreen.Resources.ContainsKey("FadeOutStoryboard"))
                {
                    var fadeOutStoryboard = (Storyboard)splashScreen.Resources["FadeOutStoryboard"];
                    fadeOutStoryboard.Begin();
                    await Task.Delay(400); // Wait for animation
                }

                await FinalizeNavigation(isFirstLaunch, args);
            }
        }

        private async Task FinalizeNavigation(bool isFirstLaunch, Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            if (isFirstLaunch)
            {
                // First Run: Go to Onboarding
                MainWindow.Content = App.GetService<OnboardingPage>();
            }
            else
            {
                // Returning User: Standard Calculator Launch
                var shellViewModel = App.GetService<ShellViewModel>();
                MainWindow.Content = new ShellPage(shellViewModel);
            }

            MainWindow.Activate();

            // Important: Notify the activation service that the UI is set
            await App.GetService<IActivationService>().ActivateAsync(args);
        }
        private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // This prevents the app from closing immediately on a non-fatal error
            // You can log the error here: System.Diagnostics.Debug.WriteLine(e.Message);

            // e.Handled = true; // Uncomment this if you want to try to recover from the crash
        }
    }
}
