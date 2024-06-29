using Inticate_Auto_Clicker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUIEx;

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

        // Restore settings.
        NotificationsCheckBox.IsChecked = NOTIFICATIONS_ENABLED;
        AlwaysOnTopCheckBox.IsChecked = ALWAYS_ON_TOP;
    }

    private void NotificationsToggled(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        NOTIFICATIONS_ENABLED = (bool)checkBox.IsChecked;
    }

    private void AlwaysOnTopToggled(object sender, RoutedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        ALWAYS_ON_TOP = (bool)checkBox.IsChecked;
        App.MainWindow.SetIsAlwaysOnTop(ALWAYS_ON_TOP);
    }
}