using System.Diagnostics;
using Inticate_Auto_Clicker.Helpers;
using Inticate_Auto_Clicker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inticate_Auto_Clicker.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    // Setting values.
    public static bool NOTIFICATIONS_ENABLED = MainWindow.SettingsController.GetSetting("NOTIFICATIONS_ENABLED");
    public static bool ALWAYS_ON_TOP = MainWindow.SettingsController.GetSetting("ALWAYS_ON_TOP");

    // Variables
    private bool isInitializing = true;

    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();

        LoadSettings();
        isInitializing = false;
    }

    private void NotificationsToggled(object sender, RoutedEventArgs e)
    {
        if (isInitializing) return;

        var checkBox = (CheckBox)sender;
        MainWindow.SettingsController.SetSetting("NOTIFICATIONS_ENABLED", (bool)checkBox.IsChecked);
        SaveSettings();
    }

    private void AlwaysOnTopToggled(object sender, RoutedEventArgs e)
    {
        if (isInitializing) return;

        var checkBox = (CheckBox)sender;
        MainWindow.SettingsController.SetSetting("ALWAYS_ON_TOP", (bool)checkBox.IsChecked);
        App.MainWindow.SetIsAlwaysOnTop((bool)checkBox.IsChecked);
        SaveSettings();
    }

    private void SaveSettings()
    {
        MainWindow.SettingsController.SetSetting("NOTIFICATIONS_ENABLED", NOTIFICATIONS_ENABLED);
        MainWindow.SettingsController.SetSetting("ALWAYS_ON_TOP", ALWAYS_ON_TOP);
    }

    private void LoadSettings()
    {
        NotificationsCheckBox.IsChecked = MainWindow.SettingsController.GetSetting("NOTIFICATIONS_ENABLED");
        AlwaysOnTopCheckBox.IsChecked = MainWindow.SettingsController.GetSetting("ALWAYS_ON_TOP");
    }
}