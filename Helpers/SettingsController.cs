using Windows.Storage;

namespace Inticate_Auto_Clicker.Helpers;
public class SettingsController
{
    // Settings
    public static bool NOTIFICATION_ENABLED;
    public static bool ALWAYS_ON_TOP;

    public SettingsController()
    {
        var localSettings = ApplicationData.Current.LocalSettings;

        // Check if settings already exist, if not, set them to their default values.
        NOTIFICATION_ENABLED = localSettings.Values["NOTIFICATION_ENABLED"] != null ? (bool)localSettings.Values["NOTIFICATION_ENABLED"] : true;
        ALWAYS_ON_TOP = localSettings.Values["ALWAYS_ON_TOP"] != null ? (bool)localSettings.Values["ALWAYS_ON_TOP"] : false;
    }

    public void SetSetting(string setting, bool value)
    {
        var localSettings = ApplicationData.Current.LocalSettings;
        localSettings.Values[setting] = value;
    }

    public bool GetSetting(string setting)
    {
        var localSettings = ApplicationData.Current.LocalSettings;
        return localSettings.Values[setting] != null ? (bool)localSettings.Values[setting] : false;
    }
}
