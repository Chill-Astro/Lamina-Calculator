using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;

namespace Lamina.Views;

// --- DATA MODELS ---
public class LaminaScript
{
    public Metadata Metadata { get; set; }
    public UIConfig UI { get; set; }
    public LogicConfig Logic { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public string FilePath { get; set; }
}

public class Metadata { public string Name { get; set; } public string Author { get; set; } public string Version { get; set; } public string Description { get; set; } public string Repo { get; set; } }
public class UIConfig { public string Formula { get; set; } public List<InputItem> Inputs { get; set; } }
public class InputItem { public string Label { get; set; } public string Key { get; set; } public string Header { get; set; } public string Placeholder { get; set; } }
public class LogicConfig { public string Output { get; set; } public string Error { get; set; } }

// --- LOGIC ---
public sealed partial class DyNamoMangerPage : Page
{
    public DyNamoMangerPage()
    {
        this.InitializeComponent();
        this.Loaded += (s, e) => LoadScripties();
    }

    public async void LoadScripties()
    {
        try
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFolder scriptiesFolder = await localFolder.CreateFolderAsync("Scripties", CreationCollisionOption.OpenIfExists);

            var files = await scriptiesFolder.GetFilesAsync();
            var list = new List<LaminaScript>();

            foreach (var file in files)
            {
                if (file.FileType.Equals(".lamina", StringComparison.OrdinalIgnoreCase))
                {
                    string json = await FileIO.ReadTextAsync(file);
                    var script = JsonSerializer.Deserialize<LaminaScript>(json);
                    if (script != null)
                    {
                        script.FilePath = file.Path;
                        list.Add(script);
                    }
                }
            }

            ScriptieList.ItemsSource = list;

            // Empty State Logic
            bool isEmpty = list.Count == 0;
            EmptyStateText.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
            ScriptieList.Visibility = isEmpty ? Visibility.Collapsed : Visibility.Visible;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"DyNamo Manager Error: {ex.Message}");
        }
    }

    private async void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.DataContext is LaminaScript script)
        {
            ContentDialog confirm = new ContentDialog
            {
                Title = "Remove Scriptie?",
                Content = $"Are you sure to do Strike Off {script.Metadata.Name}?",
                PrimaryButtonText = "Remove",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            if (await confirm.ShowAsync() == ContentDialogResult.Primary)
            {
                try
                {
                    var file = await StorageFile.GetFileFromPathAsync(script.FilePath);
                    await file.DeleteAsync();

                    LoadScripties(); // Refresh self

                    // Sync with Shell Sidebar
                    if (App.MainWindow.Content is ShellPage shell)
                    {
                        shell.RefreshImportedList();
                    }
                }
                catch { }
            }
        }
    }

    private async void Repo_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string url && !string.IsNullOrWhiteSpace(url))
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }
    }

    private async void Contribute_Click(object sender, RoutedEventArgs e)
    {
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Module-Template"));
    }

    private async void Download_Click(object sender, RoutedEventArgs e)
    {        
        await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/Chill-Astro/Lamina-Modules-Repo"));
    }
}