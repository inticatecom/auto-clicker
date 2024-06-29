using System.Runtime.InteropServices;
using DiscordRPC;
using DiscordRPC.Logging;
using Inticate_Auto_Clicker.Helpers;
using Inticate_Auto_Clicker.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Inticate_Auto_Clicker.Views;

public sealed partial class MainPage : Page
{
    // Settings
    public static bool IS_CLICKING = false;
    public static int CLICK_INTERVAL = 1000;

    // Key binds
    public static uint LEFT_MOUSE_DOWN = 0x02;
    public static uint LEFT_MOUSE_UP = 0x04;
    public static uint RIGHT_MOUSE_DOWN = 0x08;
    public static uint RIGHT_MOUSE_UP = 0x10;
    public static uint MIDDLE_MOUSE_DOWN = 0x20;
    public static uint MIDDLE_MOUSE_UP = 0x40;
    public static int START_HOTKEY = 0x75;
    public static int STOP_HOTKEY = 0x76;

    // Classes
    private static AutoClickerController clickerController;

    // Imports
    [DllImport("user32.dll")]
    private static extern void
        mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo); // Mouse click

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey); // Hotkey

    public MainViewModel ViewModel
    {
        get;
    }

    // Initialize page.
    public MainPage()
    {
        // Get view model and initialize the component.
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        // Initialize the auto clicker controller.
        clickerController = new AutoClickerController(StartBtn, StopBtn, StatusTextBlock, XamlRoot, () => IS_CLICKING,
            value => IS_CLICKING = value);
        _ = ClickLoop();
    }

    // Button click events.
    private void StartBtnClick(object sender, RoutedEventArgs e)
    {
        clickerController.SetClicking(true);
    }

    private void StopBtnClick(object sender, RoutedEventArgs e)
    {
        clickerController.SetClicking(false);
    }

    // Mouse click events.
    private static void MouseClick(String type)
    {
        switch (type)
        {
            case "left":
                mouse_event(LEFT_MOUSE_DOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event(LEFT_MOUSE_UP, 0, 0, 0, IntPtr.Zero);
                break;
            case "right":
                mouse_event(RIGHT_MOUSE_DOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event(RIGHT_MOUSE_UP, 0, 0, 0, IntPtr.Zero);
                break;
            case "middle":
                mouse_event(MIDDLE_MOUSE_DOWN, 0, 0, 0, IntPtr.Zero);
                mouse_event(MIDDLE_MOUSE_UP, 0, 0, 0, IntPtr.Zero);
                break;
        }
    }

    // Settings events.
    private void IntervalChanged(object sender, NumberBoxValueChangedEventArgs e)
    {
        var content = (NumberBox)sender;

        CLICK_INTERVAL = (int)Math.Round(content.Value);
    }

    private void KeyChanged(object sender, SelectionChangedEventArgs e)
    {
        Console.WriteLine("Key changed.");
    }

    // Click loop.
    public static async Task ClickLoop()
    {
        while (true)
        {
            // Check if hotkeys are pressed.
            if (GetAsyncKeyState(START_HOTKEY) < 0 && !IS_CLICKING)
            {
                clickerController.SetClicking(true);
            }
            else if (GetAsyncKeyState(STOP_HOTKEY) < 0 && IS_CLICKING)
            {
                clickerController.SetClicking(false);
            }

            // Check if clicking is toggled.
            if (IS_CLICKING)
            {
                MouseClick("left");
                await Task.Delay(CLICK_INTERVAL);
            }
            else
            {
                await Task.Delay(100);
            }
        }
    }
}