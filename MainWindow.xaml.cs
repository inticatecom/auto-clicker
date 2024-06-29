using DiscordRPC.Logging;
using DiscordRPC;
using Inticate_Auto_Clicker.Helpers;
using Windows.UI.ViewManagement;

namespace Inticate_Auto_Clicker;

public sealed partial class MainWindow : WindowEx
{
    private Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;
    private UISettings settings;
    public DiscordRpcClient DiscordClient;

    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();

        // Set window properties.
        this.SetWindowSize(444, 367);
        this.SetIsResizable(false);
        this.SetIsMaximizable(false);

        // Setup Discord RPC
        DiscordClient = new DiscordRpcClient("1231110592165707828");
        DiscordClient.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
        DiscordClient.Initialize();
        DiscordClient.SetPresence(new RichPresence()
        {
            Details = "Auto Clicker",
            State = "Idle",
            Assets = new Assets()
            {
                LargeImageKey = "icon",
                LargeImageText = "Inticate Auto Clicker",
            }
        });

        Closed += (sender, args) =>
        {
            DiscordClient.Dispose();
        };

        // Theme change code picked from https://github.com/microsoft/WinUI-Gallery/pull/1239
        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        settings = new UISettings();
        settings.ColorValuesChanged +=
            Settings_ColorValuesChanged; // cannot use FrameworkElement.ActualThemeChanged event
    }

    // this handles updating the caption button colors correctly when indows system theme is changed
    // while the app is open
    private void Settings_ColorValuesChanged(UISettings sender, object args)
    {
        // This calls comes off-thread, hence we will need to dispatch it to current app's thread
        dispatcherQueue.TryEnqueue(() =>
        {
            TitleBarHelper.ApplySystemThemeToCaptionButtons();
        });
    }
}