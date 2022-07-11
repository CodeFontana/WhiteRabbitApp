using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WhiteRabbit.Commands;

namespace WhiteRabbit.ViewModels;
public class MainViewModel : ViewModelBase
{
    private NotifyIcon _notifyIcon = new();

    public MainViewModel()
    {
        NavigateCommand = new NavigateCommand(this);
        NavigateCommand.Execute("Home");
        WindowState = WindowState.Normal;
        ShowInTaskbar = true;

        // Initialize notification icon
        Stream iconStream = System.Windows.Application.GetResourceStream(new Uri("Resources/WhiteRabbit.ico", UriKind.Relative)).Stream;
        _notifyIcon.Icon = new Icon(iconStream);
        _notifyIcon.Text = "WhiteRabbit Active";

        // On notification icon single-click
        _notifyIcon.Click +=
            delegate (object notifySendersender, EventArgs args)
            {
                WindowState = WindowState.Normal;
            };

        ContinuousDisplay();
    }

    private ViewModelBase _currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get
        {
            return _currentViewModel;
        }

        set
        {
            _currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    private WindowState _windowState;
    public WindowState WindowState
    {
        get
        {
            return _windowState;
        }
        set
        {
            _windowState = value;

            if (_windowState == WindowState.Minimized)
            {
                _notifyIcon.Visible = true;
                ShowInTaskbar = false;
            }
            else
            {
                _notifyIcon.Visible = false;
                ShowInTaskbar = true;
            }

            OnPropertyChanged(nameof(WindowState));
        }
    }

    private bool _showInTaskbar;
    public bool ShowInTaskbar
    {
        get
        {
            return _showInTaskbar;
        }
        set
        {
            _showInTaskbar = value;
            OnPropertyChanged(nameof(ShowInTaskbar));
        }
    }

    public ICommand NavigateCommand { get; set; }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x00000040,
        ES_CONTINUOUS = 0x80000000,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_SYSTEM_REQUIRED = 0x00000001
    }

    public static void ContinuousDisplay()
    {
        // Forces the display to be ON by resetting the display idle timer
        SetThreadExecutionState(
            EXECUTION_STATE.ES_CONTINUOUS |
            EXECUTION_STATE.ES_DISPLAY_REQUIRED |
            EXECUTION_STATE.ES_SYSTEM_REQUIRED);
    }
}
