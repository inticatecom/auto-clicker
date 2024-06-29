using System.Diagnostics;
using Inticate_Auto_Clicker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inticate_Auto_Clicker.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    // Setting values.
    public static bool NOTIFICATIONS_ENABLED = true;
    public static bool ALWAYS_ON_TOP = false;

    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();

        LoadSettings();
    }

    private void NotificationsToggled(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        NOTIFICATIONS_ENABLED = (bool)checkBox.IsChecked;
        SaveSettings();
    }

    private void AlwaysOnTopToggled(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        ALWAYS_ON_TOP = (bool)checkBox.IsChecked;
        App.MainWindow.SetIsAlwaysOnTop(ALWAYS_ON_TOP);
        SaveSettings();
    }

    private void SaveSettings()
    {
        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        localSettings.Values["NOTIFICATIONS_ENABLED"] = NOTIFICATIONS_ENABLED;
        localSettings.Values["ALWAYS_ON_TOP"] = ALWAYS_ON_TOP;
        Debug.WriteLine("Settings saved.");
    }

    private void LoadSettings()
    {
        var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        if (localSettings.Values.TryGetValue("NOTIFICATIONS_ENABLED", out var notificationsEnabled))
        {
            NotificationsCheckBox.IsChecked = (bool)notificationsEnabled;
            Debug.WriteLine("Notifications enabled: " + notificationsEnabled);
        }

        if (localSettings.Values.TryGetValue("ALWAYS_ON_TOP", out var alwaysOnTop))
        {
            AlwaysOnTopCheckBox.IsChecked = (bool)alwaysOnTop;
            Debug.WriteLine("Always on top: " + alwaysOnTop);
        }

        Debug.WriteLine("Settings loaded");
    }
}