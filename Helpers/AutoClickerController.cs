using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppNotifications.Builder;
using Microsoft.Windows.AppNotifications;

namespace Inticate_Auto_Clicker.Helpers;

public class AutoClickerController
{
    private readonly Button StartBtn;
    private readonly Button StopBtn;
    private readonly TextBlock StatusTextBlock;
    private readonly XamlRoot XamlRoot;
    private readonly Func<bool> GetIsClicking;
    private readonly Action<bool> SetIsClicking;

    public AutoClickerController(Button startBtn, Button stopBtn, TextBlock statusTextBlock, XamlRoot xamlRoot,
        Func<bool> getIsClicking, Action<bool> setIsClicking)
    {
        StartBtn = startBtn;
        StopBtn = stopBtn;
        StatusTextBlock = statusTextBlock;
        XamlRoot = xamlRoot;
        GetIsClicking = getIsClicking;
        SetIsClicking = setIsClicking;
    }

    public void SetClicking(bool state)
    {
        SetIsClicking(state);
        StatusTextBlock.Text = state ? "Status: Running" : "Status: Stopped";
        Console.WriteLine(state);
        if (state)
        {
            StartBtn.IsEnabled = false;
            StopBtn.IsEnabled = true;
            var toast = new AppNotificationBuilder().AddText("Clicking Enabled")
                .AddText("The auto clicker has started.").BuildNotification();
            AppNotificationManager.Default.Show(toast);
        }
        else
        {
            StartBtn.IsEnabled = true;
            StopBtn.IsEnabled = false;
            var toast = new AppNotificationBuilder().AddText("Clicking Disabled")
                .AddText("The auto clicker has stopped.").BuildNotification();
            AppNotificationManager.Default.Show(toast);
        }
    }
}